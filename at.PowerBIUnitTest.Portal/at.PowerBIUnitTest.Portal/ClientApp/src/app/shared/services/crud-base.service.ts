import { HttpClient } from "@angular/common/http";
import ODataStore from "devextreme/data/odata/store";
import { ODataService } from "./odata.service";


export abstract class CrudBaseService<Model> {
  abstract getStore(): ODataStore;

  public add (newRecord: Model): Promise<void> {
    return this.getStore().insert(newRecord);
  }

  public remove (id: number): Promise<void> {
    return this.getStore().remove(id);
  }

  public update (id: number, changes: Model): Promise<void> {
    return this.getStore().update(id, changes);
  }
}
