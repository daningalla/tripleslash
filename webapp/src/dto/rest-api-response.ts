import type { ResponseBase } from "@/dto/response-base";
import { Observable, of } from "rxjs";

export interface RestApiResponse<TResult> extends ResponseBase {
  result?: TResult;
}

export function ofEmpty<TResult>(): Observable<RestApiResponse<TResult>> {
  return of({
    result: undefined,
    traceId: "",
    message: "",
    errorData: undefined,
  });
}
