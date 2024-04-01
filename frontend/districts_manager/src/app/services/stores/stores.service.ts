import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StoreModel, StoreService } from '../../generated';

@Injectable({
  providedIn: 'root'
})
export class StoresService {
  constructor(private api: StoreService) { }

  getShopsForDistrict(id: number): Observable<StoreModel[]> {
    return this.api.getStoresInDistrict(id);
  }
}
