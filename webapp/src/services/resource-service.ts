import type { IEnvironment } from "@/services/environment";
import type { ServiceIndex } from "@/dto/service-index";
import { mergeMap, Observable, of, switchMap, tap } from "rxjs";
import { fromFetch } from "rxjs/fetch";
import { HttpUtils } from "@/services/http-utils";
import { environment } from "@/services/environment";
import type { SearchResult, SearchParams } from "@/dto/search";
import type { RestApiResponse } from "@/dto/rest-api-response";

/**
 * Represents a service that manages access to REST resources
 */
export class ResourceService {
  private readonly environment: IEnvironment;
  private serviceIndex?: ServiceIndex;

  constructor(environment: IEnvironment) {
    this.environment = environment;
  }

  /**
   * Performs search
   * @param term The search term
   * @param page Zero-based page number
   * @param size Maximum number of records to show
   * @param preRelease Whether to include pre-release
   */
  search(
    term: string,
    page: number,
    size: number,
    preRelease: boolean
  ): Observable<RestApiResponse<SearchResult>> {
    return this.getResult<SearchParams, RestApiResponse<SearchResult>>(
      "SearchService",
      {
        eco: "dotnet",
        term,
        pg: page,
        sz: size,
        pre: preRelease,
      }
    );
  }

  /**
   * Gets a resource result
   * @param resourceId One or more resource segments
   * @param query Optional query parameters
   */
  getResult<TParams, TResult>(
    resourceId: string,
    query?: TParams
  ): Observable<TResult> {
    const environment = this.environment;
    return this.getServiceIndex().pipe(
      mergeMap((result: ServiceIndex) => {
        const search = result.resources.find(
          (resource) => resource.type == resourceId
        );
        if (!search) {
          throw new Error("Could not find search resource");
        }
        const searchUrl = HttpUtils.makeResourceUrl(
          environment,
          [search.id],
          query
        );
        return fromFetch(searchUrl);
      }),
      switchMap((response) => response.json())
    );
  }

  private getServiceIndex(): Observable<ServiceIndex> {
    return this.serviceIndex ? of(this.serviceIndex) : this.fetchServiceIndex();
  }

  private fetchServiceIndex(): Observable<ServiceIndex> {
    const url = HttpUtils.makeResourceUrl(this.environment, ["service"]);
    return fromFetch(url).pipe(
      switchMap((response) => {
        return response.json();
      }),
      tap((resource: ServiceIndex) => (this.serviceIndex = resource))
    );
  }
}

export const resourceService = new ResourceService(environment);
