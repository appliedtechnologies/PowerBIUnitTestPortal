import { UserStory } from "./UserStory.model";

export class Structur {
    Typ?: string;
  
    Name?: string;
  
    WorkspacePbId?: string;
  
    DatasetPbId?: string;
  
    ExpectedResult?: string;
  
    DAX?: string;

    Timestamp?: string;

    LastResult?: string;
  
    Id?: number;

    UserStory?: number;

    UserStoryNavigation?: UserStory;
  
    items?: Structur[];
}