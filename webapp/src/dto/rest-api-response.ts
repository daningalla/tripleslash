import type { ResponseBase } from "@/dto/response-base";

export interface RestApiResponse<TResult> extends ResponseBase {
  result?: TResult;
}
