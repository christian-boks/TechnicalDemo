import * as jspb from 'google-protobuf'

import * as google_protobuf_empty_pb from 'google-protobuf/google/protobuf/empty_pb'; // proto import: "google/protobuf/empty.proto"


export class GetAllDistrictsReply extends jspb.Message {
  getListList(): Array<District>;
  setListList(value: Array<District>): GetAllDistrictsReply;
  clearListList(): GetAllDistrictsReply;
  addList(value?: District, index?: number): District;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetAllDistrictsReply.AsObject;
  static toObject(includeInstance: boolean, msg: GetAllDistrictsReply): GetAllDistrictsReply.AsObject;
  static serializeBinaryToWriter(message: GetAllDistrictsReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetAllDistrictsReply;
  static deserializeBinaryFromReader(message: GetAllDistrictsReply, reader: jspb.BinaryReader): GetAllDistrictsReply;
}

export namespace GetAllDistrictsReply {
  export type AsObject = {
    listList: Array<District.AsObject>,
  }
}

export class GetAllSalesPersonsForDistrictRequest extends jspb.Message {
  getDistrictid(): number;
  setDistrictid(value: number): GetAllSalesPersonsForDistrictRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetAllSalesPersonsForDistrictRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetAllSalesPersonsForDistrictRequest): GetAllSalesPersonsForDistrictRequest.AsObject;
  static serializeBinaryToWriter(message: GetAllSalesPersonsForDistrictRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetAllSalesPersonsForDistrictRequest;
  static deserializeBinaryFromReader(message: GetAllSalesPersonsForDistrictRequest, reader: jspb.BinaryReader): GetAllSalesPersonsForDistrictRequest;
}

export namespace GetAllSalesPersonsForDistrictRequest {
  export type AsObject = {
    districtid: number,
  }
}

export class GetAllSalesPersonsForDistrictReply extends jspb.Message {
  getListList(): Array<SalesPerson>;
  setListList(value: Array<SalesPerson>): GetAllSalesPersonsForDistrictReply;
  clearListList(): GetAllSalesPersonsForDistrictReply;
  addList(value?: SalesPerson, index?: number): SalesPerson;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetAllSalesPersonsForDistrictReply.AsObject;
  static toObject(includeInstance: boolean, msg: GetAllSalesPersonsForDistrictReply): GetAllSalesPersonsForDistrictReply.AsObject;
  static serializeBinaryToWriter(message: GetAllSalesPersonsForDistrictReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetAllSalesPersonsForDistrictReply;
  static deserializeBinaryFromReader(message: GetAllSalesPersonsForDistrictReply, reader: jspb.BinaryReader): GetAllSalesPersonsForDistrictReply;
}

export namespace GetAllSalesPersonsForDistrictReply {
  export type AsObject = {
    listList: Array<SalesPerson.AsObject>,
  }
}

export class GetAllStoresForDistrictRequest extends jspb.Message {
  getDistrictid(): number;
  setDistrictid(value: number): GetAllStoresForDistrictRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetAllStoresForDistrictRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetAllStoresForDistrictRequest): GetAllStoresForDistrictRequest.AsObject;
  static serializeBinaryToWriter(message: GetAllStoresForDistrictRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetAllStoresForDistrictRequest;
  static deserializeBinaryFromReader(message: GetAllStoresForDistrictRequest, reader: jspb.BinaryReader): GetAllStoresForDistrictRequest;
}

export namespace GetAllStoresForDistrictRequest {
  export type AsObject = {
    districtid: number,
  }
}

export class GetAllStoresForDistrictReply extends jspb.Message {
  getListList(): Array<Store>;
  setListList(value: Array<Store>): GetAllStoresForDistrictReply;
  clearListList(): GetAllStoresForDistrictReply;
  addList(value?: Store, index?: number): Store;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetAllStoresForDistrictReply.AsObject;
  static toObject(includeInstance: boolean, msg: GetAllStoresForDistrictReply): GetAllStoresForDistrictReply.AsObject;
  static serializeBinaryToWriter(message: GetAllStoresForDistrictReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetAllStoresForDistrictReply;
  static deserializeBinaryFromReader(message: GetAllStoresForDistrictReply, reader: jspb.BinaryReader): GetAllStoresForDistrictReply;
}

export namespace GetAllStoresForDistrictReply {
  export type AsObject = {
    listList: Array<Store.AsObject>,
  }
}

export class AddSalesPersonToDistrictRequest extends jspb.Message {
  getDistrictid(): number;
  setDistrictid(value: number): AddSalesPersonToDistrictRequest;

  getSalespersonid(): number;
  setSalespersonid(value: number): AddSalesPersonToDistrictRequest;

  getIsprimary(): boolean;
  setIsprimary(value: boolean): AddSalesPersonToDistrictRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AddSalesPersonToDistrictRequest.AsObject;
  static toObject(includeInstance: boolean, msg: AddSalesPersonToDistrictRequest): AddSalesPersonToDistrictRequest.AsObject;
  static serializeBinaryToWriter(message: AddSalesPersonToDistrictRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AddSalesPersonToDistrictRequest;
  static deserializeBinaryFromReader(message: AddSalesPersonToDistrictRequest, reader: jspb.BinaryReader): AddSalesPersonToDistrictRequest;
}

export namespace AddSalesPersonToDistrictRequest {
  export type AsObject = {
    districtid: number,
    salespersonid: number,
    isprimary: boolean,
  }
}

export class RemoveSalesPersonFromDistrictRequest extends jspb.Message {
  getDistrictid(): number;
  setDistrictid(value: number): RemoveSalesPersonFromDistrictRequest;

  getSalespersonid(): number;
  setSalespersonid(value: number): RemoveSalesPersonFromDistrictRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): RemoveSalesPersonFromDistrictRequest.AsObject;
  static toObject(includeInstance: boolean, msg: RemoveSalesPersonFromDistrictRequest): RemoveSalesPersonFromDistrictRequest.AsObject;
  static serializeBinaryToWriter(message: RemoveSalesPersonFromDistrictRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): RemoveSalesPersonFromDistrictRequest;
  static deserializeBinaryFromReader(message: RemoveSalesPersonFromDistrictRequest, reader: jspb.BinaryReader): RemoveSalesPersonFromDistrictRequest;
}

export namespace RemoveSalesPersonFromDistrictRequest {
  export type AsObject = {
    districtid: number,
    salespersonid: number,
  }
}

export class SalesPerson extends jspb.Message {
  getId(): number;
  setId(value: number): SalesPerson;

  getName(): string;
  setName(value: string): SalesPerson;

  getIsprimary(): boolean;
  setIsprimary(value: boolean): SalesPerson;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): SalesPerson.AsObject;
  static toObject(includeInstance: boolean, msg: SalesPerson): SalesPerson.AsObject;
  static serializeBinaryToWriter(message: SalesPerson, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): SalesPerson;
  static deserializeBinaryFromReader(message: SalesPerson, reader: jspb.BinaryReader): SalesPerson;
}

export namespace SalesPerson {
  export type AsObject = {
    id: number,
    name: string,
    isprimary: boolean,
  }
}

export class District extends jspb.Message {
  getId(): number;
  setId(value: number): District;

  getName(): string;
  setName(value: string): District;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): District.AsObject;
  static toObject(includeInstance: boolean, msg: District): District.AsObject;
  static serializeBinaryToWriter(message: District, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): District;
  static deserializeBinaryFromReader(message: District, reader: jspb.BinaryReader): District;
}

export namespace District {
  export type AsObject = {
    id: number,
    name: string,
  }
}

export class Store extends jspb.Message {
  getId(): number;
  setId(value: number): Store;

  getDistrictid(): number;
  setDistrictid(value: number): Store;

  getCity(): string;
  setCity(value: string): Store;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Store.AsObject;
  static toObject(includeInstance: boolean, msg: Store): Store.AsObject;
  static serializeBinaryToWriter(message: Store, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Store;
  static deserializeBinaryFromReader(message: Store, reader: jspb.BinaryReader): Store;
}

export namespace Store {
  export type AsObject = {
    id: number,
    districtid: number,
    city: string,
  }
}

