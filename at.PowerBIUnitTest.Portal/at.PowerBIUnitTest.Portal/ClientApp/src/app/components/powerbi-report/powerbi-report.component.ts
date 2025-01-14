import { Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import * as pbi from 'powerbi-client';
import { PowerbiService } from 'src/app/shared/services/report.service';

@Component({
  selector: 'app-powerbi-report',
  templateUrl: './powerbi-report.component.html',
  styleUrls: ['./powerbi-report.component.css']
})
export class PowerbiReportComponent implements AfterViewInit {
  @ViewChild('reportContainer', { static: false }) reportContainer!: ElementRef;

  constructor(private powerbiService: PowerbiService) {}

  ngAfterViewInit() {
    // Hole den Token vom Backend
    this.powerbiService.getEmbedToken2().then(
      (accessToken) => {
        const embedConfig: pbi.IEmbedConfiguration = {
          type: 'report',
          id: 'e91f92b4-7566-4f75-b171-cd0590b15060',
          embedUrl: 'https://app.powerbi.com/reportEmbed',
          accessToken: accessToken, // Dynamischer Token
          tokenType: pbi.models.TokenType.Embed,
          settings: {
            filterPaneEnabled: false,
            navContentPaneEnabled: true,
          },
        };
  
        // Power BI Dienst instanziieren
        const powerbi = new pbi.service.Service(
          pbi.factories.hpmFactory,
          pbi.factories.wpmpFactory,
          pbi.factories.routerFactory
        );
  
        // Bericht einbetten
        powerbi.embed(this.reportContainer.nativeElement, embedConfig);
      },
      (error) => {
        console.error('Fehler beim Abrufen des Embed Tokens:', error);
      }
    );
  }
  
}


