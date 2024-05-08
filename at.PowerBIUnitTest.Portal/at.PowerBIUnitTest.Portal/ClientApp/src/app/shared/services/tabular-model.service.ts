import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ODataStore from "devextreme/data/odata/store";
import { ODataService } from "./odata.service";
import { CrudBaseService } from "./crud-base.service";
import { TabularModel } from "../models/tabular-model.model";

@Injectable()
export class TabularModelService extends CrudBaseService<TabularModel>{

  constructor(
    private odataService: ODataService,
    private http: HttpClient
  ) {
    super();
  }

  getStore(): ODataStore {
    return this.odataService.context["TabularModels"];
  }
}