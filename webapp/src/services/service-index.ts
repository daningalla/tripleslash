/**
 * Provides the service index.
 */
import { Observable, of, switchMap, tap } from "rxjs";
import { fromFetch } from "rxjs/fetch";
import { HttpUtils } from "@/services/http-utils";
import type { IEnvironment } from "@/services/environment";
import { environment } from "@/services/environment";

/**
 * Manages the service index API.
 */
class ServiceIndex {
  private readonly environment: IEnvironment;
  private cached?: ServiceIndex;

  constructor(environment: IEnvironment) {
    this.environment = environment;
  }

  /**
   * Gets the service index
   */
  getResource(): Observable<ServiceIndex> {
    return this.cached ? of(this.cached) : this.fetch();
  }

  private fetch(): Observable<ServiceIndex> {
    const url = HttpUtils.makeResourceUrl(this.environment, ["service"]);
    return fromFetch(url).pipe(
      switchMap((response) => {
        return response.json();
      }),
      tap((resource: ServiceIndex) => (this.cached = resource))
    );
  }
}

export const serviceIndex = new ServiceIndex(environment);
