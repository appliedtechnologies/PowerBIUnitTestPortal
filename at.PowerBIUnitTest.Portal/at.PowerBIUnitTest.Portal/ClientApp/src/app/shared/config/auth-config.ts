import {
  MsalGuardConfiguration,
  MsalInterceptorConfiguration,
} from "@azure/msal-angular";
import {
  BrowserCacheLocation,
  EndSessionRequest,
  InteractionType,
  IPublicClientApplication,
  LogLevel,
  PublicClientApplication,
  RedirectRequest,
} from "@azure/msal-browser";
import { AppConfig } from "./app.config";


const isIE =
  window.navigator.userAgent.indexOf("MSIE ") > -1 ||
  window.navigator.userAgent.indexOf("Trident/") > -1;

export function loggerCallback(logLevel: LogLevel, message: string) {
  console.log(message);
}


export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    

   

    auth: {
      
      //howto create azure app registration: https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-spa-app-registration
      clientId: AppConfig.settings.azure.applicationId,   //  <--- insert client id here
      authority: "https://login.microsoftonline.com/organizations",
      redirectUri: location.origin,
      postLogoutRedirectUri: location.origin,
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: isIE, // set to true for IE 11
    },
    system: {
      loggerOptions: {
        loggerCallback,
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false,
      },
    },
  });
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();
  protectedResourceMap.set("https://graph.microsoft.com/v1.0/*", ["user.read"]);
  protectedResourceMap.set("https://api.powerbi.com/v1.0/*", []);

  protectedResourceMap.set(location.origin + "/odata/*", [
    `api://${AppConfig.settings.azure.applicationId}/access_as_user`,  //  <--- insert client id here
  ]);

  return {
    interactionType: InteractionType.Redirect,
    authRequest: InitRedirctRequest,
    protectedResourceMap,
  };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: InitRedirctRequest,
  };
}

export const InitRedirctRequest: RedirectRequest = {
  scopes: ["https://management.azure.com//user_impersonation"],
  extraScopesToConsent: ["https://analysis.windows.net/powerbi/api/Workspace.Read.All", "https://analysis.windows.net/powerbi/api/Dataset.Read.All"]
};
