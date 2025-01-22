import { BrowserModule } from "@angular/platform-browser";
import { APP_INITIALIZER, NgModule } from "@angular/core";
import {
  HttpClient,
  HttpClientModule,
  HTTP_INTERCEPTORS,
} from "@angular/common/http";
import {
  DxDataGridModule,
  DxDrawerModule,
  DxLoadPanelModule,
  DxToolbarModule,
  DxSelectBoxModule,
  DxFormModule,
  DxPopupModule,
  DxButtonModule,
  DxTreeViewModule,
  DxTextAreaModule,
  DxScrollViewModule,
} from "devextreme-angular";

import {
  MsalModule,
  MsalInterceptor,
  MsalRedirectComponent,
  MsalService,
  MsalGuard,
  MsalBroadcastService,
  MSAL_INSTANCE,
  MSAL_GUARD_CONFIG,
  MSAL_INTERCEPTOR_CONFIG,
} from "@azure/msal-angular";

import { AppComponent } from "./app.component";
import { HomeComponent } from "./components/home/home.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { UserComponent } from "./components/users/users.component";


import {
  MSALGuardConfigFactory,
  MSALInstanceFactory,
  MSALInterceptorConfigFactory,
} from "./shared/config/auth-config";
import { routes } from "./app-routing.module";
import { UserService } from "./shared/services/user.service";
import { LogService } from "./shared/services/log.service";
import { TenantService } from "./shared/services/tenant.service";
import { AppConfig } from "./shared/config/app.config";
import { ODataService } from "./shared/services/odata.service";

import devextremeAjax from "devextreme/core/utils/ajax";
import { sendRequestFactory } from "./shared/helper/ng-http-client-helper";
import { LayoutService } from "./shared/services/layout.service";
import { DxTreeListModule } from "devextreme-angular";
//import { DxDataGridModule } from "devextreme-angular";
import { RoleGuard } from "./shared/guards/role.guard";
import { Router, RouterModule, provideRouter } from "@angular/router";
import { UnitTestService } from "./shared/services/unit-test.service";
import { UserStoryService } from "./shared/services/user-story.service";
import { WorkspaceService } from "./shared/services/workspace.service";
import { TabularModelService } from "./shared/services/tabular-model.service";
import { SideNavigationMenuComponent } from "./components/side-navigation-menu/side-navigation-menu.component";
import { UnitTestsComponent } from "./components/unit-tests/unit-tests.component";
import { GetFirstElementPipe } from "./shared/pipes/get-first-element.pipe";
import { licenseKey } from "src/devextreme-license";
import config from "devextreme/core/config";
import { HistoryComponent } from "./components/history/history.component";
import { TestRunCollectionService } from "./shared/services/test-run-collection.service";
import { WorkspacesComponent } from "./components/workspaces/workspaces.component";
import { PowerbiReportComponent } from "./components/powerbi-report/powerbi-report.component";
import { ReportListComponent } from "./components/ReportList/ReportList.Component";
import { PowerbiService } from "./shared/services/report.service";

export function initializeAppConfig(appConfig: AppConfig, router: Router) {
  return () => appConfig.load();
}

config({ licenseKey });  

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProfileComponent,
    UserComponent,
    UnitTestsComponent,
    SideNavigationMenuComponent,
    HistoryComponent,
    GetFirstElementPipe,
    WorkspacesComponent,
    PowerbiReportComponent,
    ReportListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    MsalModule,
    RouterModule,
    DxDataGridModule,
    DxDrawerModule,
    DxToolbarModule,
    DxLoadPanelModule,
    DxTreeListModule,
    DxTreeViewModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxFormModule,
    DxPopupModule,
    DxScrollViewModule,
    DxTextAreaModule,
    DxDataGridModule
  ],
  providers: [
    provideRouter(routes),
    AppConfig,
    LogService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeAppConfig,
      deps: [AppConfig, Router],
      multi: true,
    },
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory,
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true,
    },
    MsalService,
    MsalGuard,
    MsalBroadcastService,
    RoleGuard,
    ODataService,
    UserService,
    LayoutService,
    TenantService,
    UnitTestService,
    UserStoryService,
    WorkspaceService,
    TabularModelService,
    TestRunCollectionService,
    PowerbiService,
  ],
  bootstrap: [AppComponent, MsalRedirectComponent],
})
export class AppModule {
  constructor(httpClient: HttpClient) {
    devextremeAjax.inject({ sendRequest: sendRequestFactory(httpClient) });
  }
}
