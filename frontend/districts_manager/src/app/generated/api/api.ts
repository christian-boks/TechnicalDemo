export * from './district.service';
import { DistrictService } from './district.service';
export * from './district.serviceInterface';
export * from './salesPerson.service';
import { SalesPersonService } from './salesPerson.service';
export * from './salesPerson.serviceInterface';
export * from './store.service';
import { StoreService } from './store.service';
export * from './store.serviceInterface';
export const APIS = [DistrictService, SalesPersonService, StoreService];
