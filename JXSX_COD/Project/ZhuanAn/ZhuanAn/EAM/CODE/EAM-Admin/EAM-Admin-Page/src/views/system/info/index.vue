<template>
  <div class="app-container home">
    <el-row :gutter="20">
      <el-col :lg="6" :sm="24" style="border-right: solid 1px gainsboro">
        <h2>EAM设备系统</h2>
        <p>系统业务的主要的功能：</p>

        <p>此系统用于管理厂区的设备、载治具、耗品的库存；登入的厂区不同，操作的数据源不同。</p>
        <!-- <p>
          <b>{{ $t('layout.currentVersion') }}:</b> <span>v{{ version }}</span>
        </p> -->

        <p></p>

        <!-- 版本信息 -->
        <el-card>
          <template #header>
            <span>系统信息</span>
          </template>
          <div class="body">
            <p>版本：{{ version }}</p>
          </div>
        </el-card>

        <!-- 开发人员 -->
        <el-card>
          <template #header>
            <span> 开发人员 </span>
          </template>
          <div class="body">
            <el-table :data="manageUser">
              <el-table-column prop="userName" label="工号" />
              <el-table-column prop="nickName" label="姓名" />
            </el-table>
          </div>
        </el-card>

        <el-card>
          <template #header>
            <span>联系信息</span>
          </template>
          <div class="body">
            <p>部门：CPBG_资讯开发三部</p>
            <p>姓名：刘文波</p>
            <p>邮箱：Wenbo.Liu2@luxshare-ict.com</p>
          </div>
        </el-card>
      </el-col>

      <!-- 更新日志 -->
      <el-col :sm="24" :lg="18">
        <h1>{{ previewInfo.title }}</h1>
        <MdPreview show-code-row-number editorId="id1" :modelValue="previewInfo.content" />
      </el-col>
    </el-row>
  </div>
</template>

<script setup name="index">
import { getArticle } from '@/api/article/article.js'
import { MdPreview } from 'md-editor-v3'
import 'md-editor-v3/lib/preview.css'
import defaultSettings from '@/settings'

const manageUser = ref([{ userName: 11011692, nickName: '刘文波' }])
const version = defaultSettings.version
// 文章详情
const previewInfo = ref({})

function handleView(cid) {
  // var link = `${previewUrl.value}${row.cid}`
  // window.open(link)
  getArticle(cid).then((res) => {
    previewInfo.value = res.data
  })
}

handleView(1)
</script>

<style scoped lang="scss">
.home {
  blockquote {
    padding: 10px 20px;
    margin: 0 0 20px;
    font-size: 17.5px;
    border-left: 5px solid #eee;
  }

  hr {
    margin-top: 20px;
    margin-bottom: 20px;
    border: 0;
    border-top: 1px solid #eee;
  }

  .col-item {
    margin-bottom: 20px;
  }

  ul {
    padding: 0;
    margin: 0;
  }

  font-family: 'open sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;
  font-size: 13px;
  color: #676a6c;
  overflow-x: hidden;

  ul {
    list-style-type: none;
  }

  h4 {
    margin-top: 0px;
  }

  h2 {
    margin-top: 10px;
    font-size: 26px;
    font-weight: 100;
  }

  p {
    margin-top: 10px;

    b {
      font-weight: 700;
    }
  }

  .update-log {
    ol {
      display: block;
      list-style-type: decimal;
      margin-block-start: 1em;
      margin-block-end: 1em;
      margin-inline-start: 0;
      margin-inline-end: 0;
      padding-inline-start: 40px;
    }
  }
}
</style>
