<!--
 * @Descripttion: (治具料号关联表/FIX_Fixture_Part)
 * @Author: (admin)
 * @Date: (2024-05-16)
-->
<template>
  <div>
    <!-- 查询条件 -->
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="治具" prop="fixtureId">
        <el-select v-model="queryParams.fixtureId" placeholder="系列,名称" clearable filterable remote :remote-method="handleQueryFixture">
          <template #header>
            <span>系列 / 名称</span>
          </template>
          <el-option v-for="item in options.fixture_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="料号" prop="partId">
        <el-input v-model="queryParams.partId" placeholder="请输入料号" />
      </el-form-item>
      <el-form-item>
        <el-button icon="search" type="primary" @click="handleQuery">{{ $t('btn.search') }}</el-button>
        <el-button icon="refresh" @click="resetQuery">{{ $t('btn.reset') }}</el-button>
      </el-form-item>
    </el-form>

    <!-- 工具区域 -->
    <el-row :gutter="15" class="mb10">
      <el-col :span="1.5">
        <el-button type="primary" v-hasPermi="['fixture:add']" plain icon="plus" @click="handleAdd">
          {{ $t('btn.add') }}
        </el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList" :columns="columns"></right-toolbar>
    </el-row>

    <el-table
      :data="dataList"
      v-loading="loading"
      ref="table"
      border
      header-cell-class-name="el-table-header-cell"
      highlight-current-row
      @sort-change="sortChange">
      <el-table-column prop="fixtureId" label="治具ID" width="90" align="center" v-if="columns.showColumn('fixtureId')" />
      <el-table-column prop="fixtureName" label="治具(系列/名称)" min-width="250" v-if="columns.showColumn('fixtureName')" />
      <el-table-column
        prop="partId"
        label="料号Id"
        min-width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('partId')" />
      <el-table-column
        prop="partNo"
        label="料号"
        min-width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('partNo')" />
      <el-table-column prop="defaultQty" label="默认数量" width="90" align="center" v-if="columns.showColumn('defaultQty')" />
      <el-table-column label="操作" width="120">
        <template #default="scope">
          <el-button
            type="success"
            size="small"
            icon="edit"
            title="编辑"
            v-hasPermi="['fixture:edit']"
            @click="handleUpdate(scope.row)"></el-button>
          <el-button
            type="danger"
            size="small"
            icon="delete"
            title="删除"
            v-hasPermi="['fixture:delete']"
            @click="handleDelete(scope.row)"></el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination :total="total" v-model:page="queryParams.pageNum" v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- 添加或修改治具料号关联表对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="24" v-if="opertype == 1">
            <el-form-item label="治具" prop="fixtureId">
              <el-select
                v-model="form.fixtureId"
                placeholder="系列,名称"
                clearable
                filterable
                remote
                :remote-method="handleQueryFixture"
                class="fullWidth">
                <template #header>
                  <span>系列 / 名称</span>
                </template>
                <el-option
                  v-for="item in options.fixture_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="item.dictValue"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="关联料号" prop="partId">
              <el-select
                v-model="form.partId"
                placeholder="请输入料号"
                clearable
                filterable
                remote
                :remote-method="handleQueryPart"
                :disabled="opertype == 2">
                <el-option
                  v-for="item in options.part_options"
                  :key="item.dictValue"
                  :label="item.dictLabel"
                  :value="Number(item.dictValue)"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="默认数量" prop="defaultQty">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="上线通知单创建时默认带出的数量">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  默认数量
                </span>
              </template>
              <el-input-number v-model.number="form.defaultQty" controls-position="right" placeholder="请输入默认数量" class="fullWidth" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup name="fixturepart">
import { listFixturePart, addFixturePart, delFixturePart, updateFixturePart, getFixturePart } from '@/api/fixture/fixturePart.js'
import { dictPart } from '@/api/basic/part.js'
import { dictFixtureBase } from '@/api/fixture/fixtureBase.js'

const props = defineProps({
  fixtureId: Number
})
const { proxy } = getCurrentInstance()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
if (props.fixtureId) showSearch.value = false
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: '',
  sortType: 'asc',
  fixtureId: props.fixtureId
})
const columns = ref([
  { visible: false, prop: 'fixtureId', label: '治具ID' },
  { visible: true, prop: 'fixtureName', label: '治具名称' },
  { visible: false, prop: 'partId', label: '料号Id' },
  { visible: true, prop: 'partNo', label: '料号' },
  { visible: true, prop: 'defaultQty', label: '默认数量' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()

function getList() {
  loading.value = true
  listFixturePart(queryParams).then((res) => {
    const { code, data } = res
    if (code == 200) {
      dataList.value = data.result
      total.value = data.totalNum
      loading.value = false
    }
  })
}

// 查询
function handleQuery() {
  queryParams.pageNum = 1
  getList()
}

// 重置查询操作
function resetQuery() {
  proxy.resetForm('queryRef')
  handleQuery()
}
// 自定义排序
function sortChange(column) {
  var sort = undefined
  var sortType = undefined

  if (column.prop != null && column.order != null) {
    sort = column.prop
    sortType = column.order
  }
  queryParams.sort = sort
  queryParams.sortType = sortType
  handleQuery()
}

/*************** form操作 ***************/
const formRef = ref()
const title = ref('')
// 操作类型 1、add 2、edit 3、view
const opertype = ref(0)
const open = ref(false)
const state = reactive({
  single: true,
  multiple: true,
  form: {},
  rules: {
    partId: [{ required: true, message: '料号不能为空', trigger: 'blur' }],
    fixtureId: [{ required: true, message: '治具ID不能为空', trigger: 'blur' }],
    defaultQty: [{ required: true, message: '默认数量不能为空', trigger: 'blur', type: 'number' }]
  },
  options: {
    //治具选项
    fixture_options: [],
    //料号
    part_options: []
  }
})

const { form, rules, options, single, multiple } = toRefs(state)

// 关闭dialog
function cancel() {
  open.value = false
  reset()
}

// 重置表单
function reset() {
  form.value = {
    fixtureId: null,
    partId: null,
    defaultQty: null
  }
  proxy.resetForm('formRef')
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加治具料号关联'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const query = { partId: row.partId, fixtureId: row.fixtureId }
  getFixturePart(query).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改治具料号关联'
      opertype.value = 2

      form.value = {
        ...data
      }

      if (data.partId) {
        const queryPartParams = {
          pageNum: 1,
          pageSize: 10,
          sort: '',
          sortType: 'asc',
          partId: data.partId
        }
        dictPart(queryPartParams).then((res) => {
          state.options.part_options = res.data.result
        })
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.partId != undefined && opertype.value === 2) {
        updateFixturePart(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addFixturePart(form.value).then((res) => {
          proxy.$modal.msgSuccess('新增成功')
          open.value = false
          getList()
        })
      }
    }
  })
}

// 删除按钮操作
function handleDelete(row) {
  const Ids = row.partId || ids.value
  const info = {
    partId: row.partId,
    fixtureId: row.fixtureId
  }
  proxy
    .$confirm('是否确认删除关联的料号 ' + info.partId + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delFixturePart(info)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 查询料号
function handleQueryPart(keyword) {
  if (keyword) {
    const queryPartParams = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      partNo: keyword
    }
    setTimeout(() => {
      dictPart(queryPartParams).then((res) => {
        state.options.part_options = res.data.result
      })
    }, 200)
  }
}

//治具查询
function handleQueryFixture(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    if (keyword.indexOf(',') >= 0) {
      const kv = keyword.split(',')
      query.series = kv[0]
      query.fixtureName = kv[1]
      query.keyword = null
    }
    setTimeout(() => {
      dictFixtureBase(query).then((res) => {
        state.options.fixture_options = res.data.result
      })
    }, 200)
  }
}

//监听属性变化
watch(props, (val) => {
  queryParams.fixtureId = props.fixtureId
  handleQuery()
})

handleQuery()
</script>
