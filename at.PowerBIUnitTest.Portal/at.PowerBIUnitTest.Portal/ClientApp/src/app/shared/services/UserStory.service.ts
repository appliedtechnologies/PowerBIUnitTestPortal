import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ODataStore from "devextreme/data/odata/store";
import { AppConfig } from "../config/app.config";
import { ODataService } from "./odata.service";
import { UserService } from "./user.service";
import { LayoutService } from "./layout.service";
import { UnitTest } from "src/app/shared/models/UnitTest.model";
import ODataContext from "devextreme/data/odata/context";
import { map } from "rxjs/operators";
import { UserStory } from "../models/UserStory.model";
@Injectable()
export class UserStoryService {

  constructor(
    private odataService: ODataService,
    //private UserStoryService: UserStoryService,
    private http: HttpClient,

  ) { }

  getStore(): ODataStore {
    return this.odataService.context["UserStories"];
  }

  public copyToOtherTabularModel(userStoryId: number, targetTabularModelId: number, targetWorkspaceId: number): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      let request = this.http
        .post(`${AppConfig.settings.api.url}/UserStories(${userStoryId})/Copy2`, {
          targetTabularModelId1: targetTabularModelId,
             targetWorkspaceId1: targetWorkspaceId,
             userStoryId1: userStoryId,
          
        })
        .subscribe({
          next: (data) => resolve(),
          error: (error) => reject(error),
        });
    });
  }
}