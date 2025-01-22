import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { ReportService } from 'src/app/shared/services/report.service';
import { UserService } from 'src/app/shared/services/user.service';
import { AppConfig } from 'src/app/shared/config/app.config';
import { filter } from 'rxjs/operators';
import { Report } from 'src/app/shared/models/report.model';

@Component({
  selector: 'app-side-navigation-menu',
  templateUrl: './side-navigation-menu.component.html',
  styleUrls: ['./side-navigation-menu.component.css'],
})
export class SideNavigationMenuComponent {
  navigationEntries: NavigationEntry[];
  version: string = AppConfig.settings.version;
  selectedItemRoute: String;
  reportsCache: Report[] | null = null;

  constructor(
    private router: Router,
    private userService: UserService,
    private reportsService: ReportService

  ) {

    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.selectedItemRoute = this.router.url.split(/[\#\?]+/)[0];
        this.setNavigationEntries();
      });

    this.setNavigationEntries();
    this.userService.stateChanged$.subscribe(() => {
      this.setNavigationEntries();
    });
  }

  onItemClickNavigation(e): void {
    if (e.itemData.routerLink) {
      this.router.navigate([e.itemData.routerLink]);
    } else {
      this.setNavigationEntries(!e.itemData.expanded);
    }
  }

  onItemExpanded(e): void {
    this.setNavigationEntries();
  }

  async setNavigationEntries(expanded: boolean = undefined): Promise<void> {
    if (expanded === undefined) {
      expanded = localStorage.getItem(
        'atPowerBiUnitTestPortal_ExpandedSideNavigation'
      ) == 'true';
    } else {
      localStorage.setItem(
        'atPowerBiUnitTestPortal_ExpandedSideNavigation',
        String(expanded)
      );
    }

    if (!this.reportsCache) {
      try {
        this.reportsCache = await this.reportsService.getStore().load();
      } catch (error) {
        console.error('Failed to load reports:', error);
        this.reportsCache = [];
      }
    }

    this.navigationEntries = [
      {
        text: 'Profile',
        icon: 'at-icon powercid-icon-benutzer',
        routerLink: '/profile',
        visible: this.userService.isLogggedIn,
      },
      {
        text: 'Unit Tests',
        icon: 'runner',
        routerLink: '/unittests',
        visible: this.userService.isLogggedIn,
      },
      {
        text: 'Reports',
        icon: 'chart',
        visible: this.userService.isLogggedIn && this.reportsCache.length > 0,
        items: this.reportsCache.map((report: Report) => ({
          text: report.Name,
          icon: 'chart',
          routerLink: `/reports/${report.WorkspaceId}/${report.ReportId}`,
          visible: this.userService.isLogggedIn,
        })),
      },
      {
        text: 'History',
        icon: 'clock',
        routerLink: '/history',
        visible: this.userService.isLogggedIn,
      },
      {
        text: 'App Settings',
        icon: 'at-icon powercid-icon-einstellungen',
        expanded: expanded,
        visible: this.userService.isLogggedIn,
        items: [
          {
            text: 'Workspaces',
            icon: 'activefolder',
            routerLink: '/workspaces',
            visible: this.userService.isLogggedIn,
          },
          {
            text: "Reports",
            icon: 'columnproperties',
            routerLink: '/reportlist',
            visible: this.userService.isLogggedIn
          }
        ],
      },
    ];

    this.navigationEntries.forEach((e) => {
      this.checkNavigationEntry(e);
      e.items?.forEach((ee) => {
        this.checkNavigationEntry(ee);
        if (ee.selected) {
          localStorage.setItem(
            'atPowerBiUnitTestPortal_ExpandedSideNavigation',
            String(true)
          );
          e.expanded = true;
        }
      });
    });
    ;
  }

  checkNavigationEntry(entry: NavigationEntry): void {
    if (entry.visible === undefined) entry.visible = false;

    if (entry.routerLink == this.selectedItemRoute) entry.selected = true;
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
