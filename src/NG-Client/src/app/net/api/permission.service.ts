/**
 * ��̨api�ĵ�
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 *//* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { AccountPermissionModel } from '../model/accountPermissionModel';
import { AccountSetPermissionRequest } from '../model/accountSetPermissionRequest';
import { PermissionGroup } from '../model/permissionGroup';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class PermissionService {

    protected basePath = '/';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * 修改用户权限
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminPermissionAccountSetPermissionPost(body?: AccountSetPermissionRequest, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public adminPermissionAccountSetPermissionPost(body?: AccountSetPermissionRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public adminPermissionAccountSetPermissionPost(body?: AccountSetPermissionRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public adminPermissionAccountSetPermissionPost(body?: AccountSetPermissionRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<any>('post',`${this.basePath}/admin/Permission/AccountSetPermission`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 获取所有的权限
     * 
     * @param accountId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminPermissionGetAllGet(accountId?: string, observe?: 'body', reportProgress?: boolean): Observable<Array<PermissionGroup>>;
    public adminPermissionGetAllGet(accountId?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<PermissionGroup>>>;
    public adminPermissionGetAllGet(accountId?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<PermissionGroup>>>;
    public adminPermissionGetAllGet(accountId?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (accountId !== undefined && accountId !== null) {
            queryParameters = queryParameters.set('accountId', <any>accountId);
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<Array<PermissionGroup>>('get',`${this.basePath}/admin/Permission/GetAll`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 获取当前用户权限
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminPermissionGetPermissionsGet(observe?: 'body', reportProgress?: boolean): Observable<AccountPermissionModel>;
    public adminPermissionGetPermissionsGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<AccountPermissionModel>>;
    public adminPermissionGetPermissionsGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<AccountPermissionModel>>;
    public adminPermissionGetPermissionsGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<AccountPermissionModel>('get',`${this.basePath}/admin/Permission/GetPermissions`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
