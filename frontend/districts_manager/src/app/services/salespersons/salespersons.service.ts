import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SalesPersonService, SalesPersonModel, AddSalesPersonRequestModel } from '../../generated';

@Injectable({
  providedIn: 'root'
})
export class SalesPersonsService {

  constructor(private api: SalesPersonService) { }

  getSalesPersonsForDistrict(id: number): Observable<SalesPersonModel[]> {
    return this.api.getSalesPersonByDistrictId(id);
  }

  addSalesPersonsToDistrict(addSalesPerson: AddSalesPersonRequestModel, districtId: number): Observable<any> {
    return this.api.addSalesPersonToDistrict(districtId, addSalesPerson);
  }

  removeSalesPersonFromDistrict(districtId: number, salesPersonId: number): Observable<any> {
    return this.api.removeSalesPersonFromDistrict(districtId, salesPersonId);
  }

}
