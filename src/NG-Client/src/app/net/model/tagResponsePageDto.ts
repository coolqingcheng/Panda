/**
 * 后台api文档
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { TagResponse } from './tagResponse';

export interface TagResponsePageDto { 
    message?: string;
    data?: Array<TagResponse>;
    total?: number;
}