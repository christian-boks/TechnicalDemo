import { Component } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { DistrictsService } from '../../services/districts/districts.service';
import { StoresComponent } from '../stores/stores.component';
import { SalespersonsComponent } from '../salespersons/salespersons.component';
import { DistrictModel } from '../../generated';

@Component({
  selector: 'app-districts',
  standalone: true,
  imports: [NgFor, NgIf, StoresComponent, SalespersonsComponent],
  templateUrl: './districts.component.html',
  styleUrl: './districts.component.css'
})
export class DistrictsComponent {

  districts: DistrictModel[] = [];
  selectedDistrict?: DistrictModel;

  constructor(private districtService: DistrictsService) {
  }

  // We want to load the list of districts as soon as we start
  ngOnInit(): void {
    this.districtService.getDistricts().subscribe(res => {
      this.districts = res;
    });
  }

  onSelect(district: DistrictModel): void {
    this.selectedDistrict = district;
  }

}
