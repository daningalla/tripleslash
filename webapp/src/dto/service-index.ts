import type { ServiceResource } from "@/dto/service-resource";

export interface ServiceIndex {
  version: string;
  ecosystems: string[];
  resources: ServiceResource[];
}
