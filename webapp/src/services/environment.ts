/**
 * Represents the current environment definition.
 */
export interface IEnvironment {
  hostUrl: string;
  serviceIndexResource: string;
}

export const environment: IEnvironment = {
  hostUrl: "https://localhost:7076",
  serviceIndexResource: "/serviceIndex",
};
