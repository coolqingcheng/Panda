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
import { AdminCategoryItem } from './adminCategoryItem';
import { PostStatus } from './postStatus';

export interface AdminPostItemResponse { 
    id?: number;
    title?: string;
    summary?: string;
    addTime?: Date;
    updateTime?: Date;
    accountName?: string;
    status?: PostStatus;
    readonly statusName?: string;
    categoryItems?: Array<AdminCategoryItem>;
    customLink?: string;
}