import { UserStory } from "./user-story.model";
import { Workspace } from "./workspace.model";

export interface TabularModel {
    Id?: number;
    UniqueIdentifier?: string;
    MsId?: string;
    Workspace?: number;
    Name?: string;
    UserStories?: UserStory[];
    WorkspaceNavigation?: Workspace;
}