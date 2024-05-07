import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { MsalGuard } from "@azure/msal-angular";
import { HomeComponent } from "./components/home/home.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { UserComponent } from "./components/users/users.component";
import { RoleGuard } from "./shared/guards/role.guard";
import { UnitTestsComponent } from "./components/unit-tests/unit-tests.component";
import { HistoryComponent } from "./components/history/history.component";

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "/unittests" },
  { path: "profile", component: ProfileComponent, canActivate: [MsalGuard] },
  { path: "unittests", component: UnitTestsComponent, canActivate: [MsalGuard] },
  { path: "history", component: HistoryComponent, canActivate: [MsalGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: "legacy" })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
