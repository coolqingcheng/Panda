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

export interface LoginStatusResult { 
    /**
     * 是否初始化Admin账号
     */
    isInit?: boolean;
    /**
     * 当前是否已经登陆
     */
    isLogin?: boolean;
}