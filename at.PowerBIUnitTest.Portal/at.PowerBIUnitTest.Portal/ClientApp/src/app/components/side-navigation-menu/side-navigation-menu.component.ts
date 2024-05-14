import { Component } from '@angular/core';
import { UserService } from "src/app/shared/services/user.service";
import { AppConfig } from 'src/app/shared/config/app.config';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-side-navigation-menu',
  templateUrl: './side-navigation-menu.component.html',
  styleUrls: ['./side-navigation-menu.component.css']
})
export class SideNavigationMenuComponent {

  navigationEntries: NavigationEntry[];
  version: string = AppConfig.settings.version;
  selectedItemRoute: String;

  constructor(private router: Router, private userService: UserService) {
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event) => {
        this.selectedItemRoute = this.router.url.split(/[\#\?]+/)[0];
        this.setNavigationEntries();
      });

    this.setNavigationEntries();
    this.userService.stateChanged$.subscribe(() => {
      this.setNavigationEntries();
    });
  }

  onItemClickNavigation(e): void {
    if (e.itemData.routerLink)
      this.router.navigate([e.itemData.routerLink]);
    else
      this.setNavigationEntries(!e.itemData.expanded);
  }

  onItemExpanded(e): void {
    this.setNavigationEntries();
  }

  setNavigationEntries(expanded: boolean = undefined): void {
    if (expanded === undefined)
      expanded = (localStorage.getItem("atPowerBiUnitTestPortal_ExpandedSideNavigation") == "true");
    else
      localStorage.setItem(
        "atPowerBiUnitTestPortal_ExpandedSideNavigation",
        String(expanded)
      );

    this.navigationEntries = [
      {
        text: "Profile",
        icon: "at-icon powercid-icon-benutzer",
        routerLink: "/profile",
        visible: this.userService.isLogggedIn
      },
      {
        text: "Unit Tests",
        icon: "runner",
        routerLink: "/unittests",
        visible: this.userService.isLogggedIn
      },
      {
        text: "History",
        icon: "clock",
        routerLink: "/history",
        visible: this.userService.isLogggedIn
      },
      {
        text: "App Settings",
        icon: "at-icon powercid-icon-einstellungen",
        expanded: expanded,
        visible: this.userService.isLogggedIn,
        items: [
          {
            text: "Workspaces",
            icon: "activefolder",
            routerLink: "/workspaces",
            visible: this.userService.isLogggedIn,
          },
        ]
      }
    ];

    this.navigationEntries.forEach(e => {
      this.checkNavigationEntry(e);
      e.items?.forEach(ee => {
        this.checkNavigationEntry(ee);
        if (ee.selected) {
          localStorage.setItem(
            "atPowerBiUnitTestPortal_ExpandedSideNavigation",
            String(true)
          );
          e.expanded = true;
        }
      })
    });
  }

  checkNavigationEntry(entry: NavigationEntry): void {
    if (entry.visible === undefined)
      entry.visible = false;

    if (entry.routerLink == this.selectedItemRoute)
      entry.selected = true;
  }
}

export class NavigationEntry {
  text: string;
  visible: boolean;
  icon: string;
  routerLink?: string;
  selected?: boolean;
  expanded?: boolean;
  items?: NavigationEntry[];
}