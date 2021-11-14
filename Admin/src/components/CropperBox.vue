<template>
    <div class="cropper-box">
       <div class="operating-area">
            <img
            class="imgview"
            ref="el"
            src="https://img1.baidu.com/it/u=2249219025,1332667262&fm=26&fmt=auto"
        />
        <div class="preview" ref="preview" ></div>
       </div>
        <input placeholder="选择图片" type="file" @change="selectFile" />
        <el-button @click="save()">保存图片</el-button>
    </div>
</template>

<script lang="ts">
import Cropper from "cropperjs"
import "cropperjs/dist/cropper.min.css"
import { onMounted, ref } from 'vue';
export default {
    name: 'cropper-box',
    setup() {
        const el = ref<HTMLImageElement>();
        const preview = ref<HTMLDivElement>();

        let cropper: Cropper;

        onMounted(() => {
            if (el.value) {
                cropper = new Cropper(el.value, {
                    preview: preview.value,
                    checkCrossOrigin: true,
                    aspectRatio: 5 / 5,
                })
            }
        })

        const selectFile = (e: any) => {
            console.log(e)

        }

        const save = ()=>{
            console.log(preview.value)
            let base64 = cropper.getCroppedCanvas().toDataURL("image/png")
            console.log(base64)
        }

        return {
            el,
            preview,
            selectFile,
            save
        }
    }
}
</script>

<style scoped lang="scss">
.imgview {
    width: 450px;
    height: 450px;
    display: block;
}
.preview {
    height: 100px;
    width: 100px;
    border: 1px solid #ccc;
    overflow: hidden;
}
.operating-area{
    display: flex;
    flex-direction: row;
    align-items: flex-end;
}
</style>