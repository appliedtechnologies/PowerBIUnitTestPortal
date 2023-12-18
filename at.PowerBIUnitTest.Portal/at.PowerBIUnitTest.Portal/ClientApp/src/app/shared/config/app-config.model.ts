export interface IAppConfig {
    logging: {
      console: boolean;
    },
    azure: {
      roleNameAdmin: string;
      applicationId: string;
    },
    api: {
      url: string;
    },
    version: string;
  }
  