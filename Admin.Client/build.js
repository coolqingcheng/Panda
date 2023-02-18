
import { exec } from 'node-exec-promise'
import path from 'path'

import fs from 'fs-extra'

const fsExits = (path) => {
    return new Promise((resolve, reject) => {
        fs.access(path).then(() => {
            resolve(true)
        }).catch(() => {
            resolve(false)
        })
    })
}


const run = async () => {
    try {
        const rootPath = path.resolve(path.resolve(), "../")
        console.log('项目根目录：', rootPath)
        let { stdout, stderr } = await exec('yarn build')
        console.log(stdout, stderr)
        //删除wwwroot/admin下的所有文件

        const serverPath = path.resolve(rootPath, 'Server', 'QingCheng.Site')
        const wwwroot_admin = path.resolve(serverPath, 'wwwroot', 'admin')
        console.log('服务器文件夹:', serverPath)
        console.log('wwwroot/admin路径:', wwwroot_admin)
        if (await fsExits(wwwroot_admin)) {
            console.log('删除wwwroot/admin目录')
            await fs.rm(wwwroot_admin, { recursive: true })
        }

        await fs.ensureDir(wwwroot_admin)

        //拷贝文件dist到wwwroot/admin下
        const dist = path.resolve(path.resolve(), 'dist')
        await fs.copy(dist, wwwroot_admin);
        await fs.rm(dist, { recursive: true })

        console.log('打包dist目录到wwwrrot/admin下完成')

        // const outDir = path.resolve(rootPath, 'publish')
        // //执行dotnet构建
        // const buildCmd = `cd ${serverPath} && dotnet publish -r win-x64 --self-contained=false -c Release QingCheng.Site.csproj -o ${outDir}`
        // console.log('构建.net 项目', buildCmd)
        // const res = await exec(buildCmd)
        // console.log(res.stdout, res.stderr)
        // console.log('打包完成')
        // if (await fsExits(outDir)) {
        //     console.log('发布目录找到了:',outDir)
        // } else {
        //     console.log('找不到发布目录。。。。。', publishDir)
        // }
        //拷贝构建文件
    } catch (error) {
        console.log(error)
    }
}

run();