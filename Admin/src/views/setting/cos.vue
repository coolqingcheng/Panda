<template>
    <el-form label-width="100px" label-position="top">
        <el-form-item label="地域简称">
            <el-input placeholder="COS 地域的简称"></el-input>
        </el-form-item>
        <el-form-item label="SecretId">
            <el-input placeholder="云 API 密钥 SecretId"></el-input>
        </el-form-item>
        <el-form-item label="SecretKey">
            <el-input placeholder="云 API 密钥 SecretKey"></el-input>
        </el-form-item>
        <el-form-item label="存储桶名称">
            <el-input placeholder="存储桶名称，此处填入格式必须为 bucketname-APPID"></el-input>
        </el-form-item>
        <el-form-item label="访问默认域名">
            <el-input placeholder="访问默认域名"></el-input>
        </el-form-item>
        <el-form-item>
            <el-button type="primary">保存</el-button>
        </el-form-item>
    </el-form>
</template>

<script>
export default {

}
</script>

<style>
</style>