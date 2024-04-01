import { Component, Input } from '@angular/core';
//import { District as DistrictModel } from '../../models/district';
import { NgIf, NgFor } from '@angular/common';
import { StoresService } from '../../services/stores/stores.service';
import { DistrictModel, StoreModel } from '../../generated';

@Component({
  selector: 'app-stores',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './stores.component.html',
  styleUrl: './stores.component.css'
})
export class StoresComponent {
  shops: StoreModel[] = [];

  private _district?: DistrictModel;
  @Input()
  public set district(val: DistrictModel | undefined) {
    this._district = val;
    this.districtChanged();
  }

  constructor(private shopsService: StoresService) {
  }

  // When the district changes we want to load all the shops for that district
  public districtChanged() {
    if (!this._district) {
      return;
    }
    if (!this._district.id) {
      return;
    }


    this.shopsService.getShopsForDistrict(this._district.id).subscribe(res => {
      this.shops = res;
    });
  }

}
