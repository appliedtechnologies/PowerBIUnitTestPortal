import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ODataStore from "devextreme/data/odata/store";
import { ODataService } from "./odata.service";

@Injectable()
export class TabularModelService {

  constructor(
    private odataService: ODataService,
    private http: HttpClient,

  ) { }

  getStore(): ODataStore {
    return this.odataService.context["TabularModels"];
  }

}