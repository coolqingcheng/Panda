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
 *//* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { TagAddRequest } from '../model/tagAddRequest';
import { TagResponse } from '../model/tagResponse';
import { TagResponsePageDto } from '../model/tagResponsePageDto';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class TagService {

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
     * 
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminTagAddOrUpdatePost(body?: TagAddRequest, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public adminTagAddOrUpdatePost(body?: TagAddRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public adminTagAddOrUpdatePost(body?: TagAddRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public adminTagAddOrUpdatePost(body?: TagAddRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


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

        return this.httpClient.request<any>('post',`${this.basePath}/admin/Tag/AddOrUpdate`,
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
     * 
     * 
     * @param index 
     * @param size 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminTagGetListGet(index: number, size: number, observe?: 'body', reportProgress?: boolean): Observable<TagResponsePageDto>;
    public adminTagGetListGet(index: number, size: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<TagResponsePageDto>>;
    public adminTagGetListGet(index: number, size: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<TagResponsePageDto>>;
    public adminTagGetListGet(index: number, size: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (index === null || index === undefined) {
            throw new Error('Required parameter index was null or undefined when calling adminTagGetListGet.');
        }

        if (size === null || size === undefined) {
            throw new Error('Required parameter size was null or undefined when calling adminTagGetListGet.');
        }

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (index !== undefined && index !== null) {
            queryParameters = queryParameters.set('Index', <any>index);
        }
        if (size !== undefined && size !== null) {
            queryParameters = queryParameters.set('Size', <any>size);
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

        return this.httpClient.request<TagResponsePageDto>('get',`${this.basePath}/admin/Tag/GetList`,
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
     * 
     * 
     * @param key 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminTagSearchTagGet(key?: string, observe?: 'body', reportProgress?: boolean): Observable<Array<TagResponse>>;
    public adminTagSearchTagGet(key?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<TagResponse>>>;
    public adminTagSearchTagGet(key?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<TagResponse>>>;
    public adminTagSearchTagGet(key?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (key !== undefined && key !== null) {
            queryParameters = queryParameters.set('key', <any>key);
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

        return this.httpClient.request<Array<TagResponse>>('get',`${this.basePath}/admin/Tag/SearchTag`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
