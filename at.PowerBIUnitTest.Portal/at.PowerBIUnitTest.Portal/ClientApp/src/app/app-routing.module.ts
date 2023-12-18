import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { MsalGuard } from "@azure/msal-angular";
import { HomeComponent } from "./components/home/home.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { UserComponent } from "./components/users/users.component";
import { utCreateComponent } from "./components/utCreate/utCreate.component";
import { utExecuteComponent } from "./components/utExecute/utExecute.component";
import { utTreeViewTestComponent } from "./components/utTreeViewTest/utTreeViewTest.component";
import { RoleGuard } from "./shared/guards/role.guard";

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "/unittests" },
  { path: "profile", component: ProfileComponent, canActivate: [MsalGuard] },
  { path: "unittests", component: utTreeViewTestComponent, canActivate: [MsalGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: "legacy" })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
