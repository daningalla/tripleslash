import type { IEnvironment } from "@/services/environment";

/**
 * Defines http utilities.
 */
export class HttpUtils {
  /**
   * Constructs an url.
   * @param environment Environment instance that defines the base url
   * @param resources Array of resources that are used to construct the url
   * @param params Optional query parameters
   */
  public static makeResourceUrl<TParams>(
    environment: IEnvironment,
    resources: string[],
    params?: TParams
  ) {
    let url = `${environment.hostUrl}`;
    let count = 0;

    resources.forEach((res) => {
      if (count > 0 || !res.startsWith("/")) {
        url += "/";
      }
      url += res;
      count++;
    });

    if (params) {
      url += "?";
      count = 0;
      const keys = Object.keys(params) as (keyof typeof params)[];
      keys.forEach((key) => {
        if (count > 0) {
          url += "&";
        }
        const value = params[key];
        url += `${key}=${value}`;
        count++;
      });
    }

    return url;
  }
}
