import { Component, EventEmitter, Input, Output } from '@angular/core';
//import { AddSalesPerson } from '../../models/addsalesperson';
import { FormsModule } from '@angular/forms';
import { SalesPersonsService } from '../../services/salespersons/salespersons.service';
import { DistrictsService } from '../../services/districts/districts.service';
import { AddSalesPersonRequestModel, DistrictModel } from '../../generated';

@Component({
  selector: 'app-addsalesperson',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './addsalesperson.component.html',
  styleUrl: './addsalesperson.component.css'
})
export class AddsalespersonComponent {
  salesperson: AddSalesPersonRequestModel = { salesPersonId: 0, isPrimary: false };

  constructor(private salespersonsService: SalesPersonsService, private districtsService: DistrictsService) {
  }

  @Input() district!: DistrictModel;
  @Output() done = new EventEmitter<boolean>();

  onCancel(): void {
    this.done.emit(false);
  }

  onSave(): void {
    if (this.district && this.district.id) {
      let req = this.salesperson;
      req.isPrimary = this.salesperson.isPrimary as any == "true" ? true : false;

      this.salespersonsService.addSalesPersonsToDistrict(req, this.district.id).subscribe(
        {
          next: () => {
            this.done.emit(true);
          },
          error: (error) => {
            alert(JSON.stringify(error));
            console.log("Got an error:", error);
          }
        }
      );

    } else {
      console.log("Missing district");
      this.done.emit(false);
    }
  }

}
