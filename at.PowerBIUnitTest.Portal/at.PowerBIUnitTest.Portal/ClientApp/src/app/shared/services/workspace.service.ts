import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ODataStore from "devextreme/data/odata/store";
import { AppConfig } from "../config/app.config";
import { ODataService } from "./odata.service";
import { UserService } from "./user.service";
import { LayoutService } from "./layout.service";
import ODataContext from "devextreme/data/odata/context";
import { map } from "rxjs/operators";
import { Workspace } from "../models/workspace.model";
import { UserStoryService } from "./user-story.service";
import { CrudBaseService } from "./crud-base.service";
@Injectable()
export class WorkspaceService extends CrudBaseService<Workspace>{
  constructor(
    private odataService: ODataService,
    private http: HttpClient
  ) {
    super();
  }

  public getStore(): ODataStore {
    return this.odataService.context["Workspaces"];
  }

  public pullWorkspaces(): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      let request = this.http
        .post(`${AppConfig.settings.api.url}/Workspaces/Pull`, {})
        .subscribe({
          next: () => resolve(),
          error: () => reject ()
        });
    });
  }
}