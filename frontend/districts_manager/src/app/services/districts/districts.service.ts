import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DistrictModel, DistrictService } from '../../generated';
import { District, GetAllDistrictsReply } from '../../generated/grpc/backend_api_pb';
import google_protobuf_empty_pb from 'google-protobuf/google/protobuf/empty_pb.js'
import { BackendApiClient } from '../../generated/grpc/Backend_apiServiceClientPb';

let useREST = true;

@Injectable({
  providedIn: 'root'
})
export class DistrictsService {
  backendApiService: BackendApiClient;

  constructor(private api: DistrictService) {
    this.backendApiService = new BackendApiClient("http://localhost:8080");
  }

  getDistricts(): Observable<DistrictModel[]> {
    if (!useREST) {
      return this.api.getAllDistricts();
    } else {
      const observable = new Observable<DistrictModel[]>((subscriber) => {
        let empty = new google_protobuf_empty_pb.Empty();

        this.backendApiService.getAllDistricts(empty).then((response: GetAllDistrictsReply) => {

          let districts: District[] = response.getListList();
          let dm: DistrictModel[] = districts.map((district) => {
            return { id: district.getId(), name: district.getName() }
          });

          subscriber.next(dm);
        });
      });

      return observable;
    }

  }

}
