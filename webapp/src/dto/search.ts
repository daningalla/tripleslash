import type { PackageId } from "@/dto/package-id";

export interface SearchResultItem {
  packageSource: string;
  packageId: PackageId;
  title: string;
  summary?: string;
  authors?: string[];
  projectUri?: string;
  repositoryUri?: string;
  repositoryType?: string;
  licenseUrl?: string;
  description?: string;
  iconUrl?: string;
  tags?: string[];
  totalDownloads?: number;
  sourceProperties?: never;
}

export interface SearchResultGroup {
  providerKey: string;
  hits: number;
  faulted: boolean;
  error?: string;
  results: SearchResultItem[];
}

export interface SearchResult {
  totalHits: number;
  providerErrors: number;
  groups: SearchResultGroup[];
}

export interface SearchParams {
  eco: string;
  term: string;
  pg: number;
  sz: number;
  pre: boolean;
}
