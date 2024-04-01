import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { SalesPersonsService } from '../../services/salespersons/salespersons.service';
import { AddsalespersonComponent } from '../addsalesperson/addsalesperson.component';
import { DistrictsService } from '../../services/districts/districts.service';
import { DistrictModel, SalesPersonModel } from '../../generated';

@Component({
  selector: 'app-salespersons',
  standalone: true,
  imports: [NgIf, NgFor, AddsalespersonComponent],
  templateUrl: './salespersons.component.html',
  styleUrl: './salespersons.component.css'
})
export class SalespersonsComponent {
  salesPersons: SalesPersonModel[] = [];
  selectedSalesPerson?: SalesPersonModel;
  isAdding: boolean = false;

  public _district!: DistrictModel;
  @Input()
  public set district(val: DistrictModel) {
    this._district = val;
    this.districtChanged();
  }

  @Output() updated = new EventEmitter<void>();

  constructor(private salespersonsService: SalesPersonsService, private districtsService: DistrictsService) {
  }

  // When the district changes we want to load all the sales persons for that district
  public districtChanged() {
    if (!this._district) {
      return;
    }

    // Make sure we clear the selection when the district has changed
    this.selectedSalesPerson = undefined;
    this.isAdding = false;

    this.updateList();
  }

  updateList() {
    if (this._district.id) {
      this.salespersonsService.getSalesPersonsForDistrict(this._district.id).subscribe(res => {
        this.salesPersons = res;
      });
    }
  }

  onSelect(salesPerson: SalesPersonModel): void {
    this.selectedSalesPerson = salesPerson;
  }

  onRemove(salesPerson: SalesPersonModel | undefined): void {
    if (!salesPerson) {
      return;
    }
    if (!salesPerson.id) {
      return;
    }
    if (!this._district) {
      return;
    }
    if (!this._district.id) {
      return;
    }

    this.salespersonsService.removeSalesPersonFromDistrict(this._district.id, salesPerson.id).subscribe(
      {
        next: () => {
          this.updateList();
        },
        error: (error) => {
          alert(JSON.stringify(error));
          console.log("Got an error:", error);
        }
      }
    );
  }

  onAdd(): void {
    this.isAdding = true;
  }

  addDone(result: boolean) {
    this.isAdding = false;
    this.updateList();
  }

}
