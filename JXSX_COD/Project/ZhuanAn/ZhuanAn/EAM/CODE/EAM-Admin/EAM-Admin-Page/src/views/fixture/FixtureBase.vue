<!--
 * @Descripttion: (治具信息/FIX_Fixture_Base)
 * @Author: (admin)
 * @Date: (2024-04-24)
-->
<template>
  <div>
    <el-form :model="queryParams" label-position="right" inline ref="queryRef" v-show="showSearch" @submit.prevent>
      <el-form-item label="治具名称" prop="fixtureName">
        <el-input v-model="queryParams.fixtureName" placeholder="请输入治具名称" />
      </el-form-item>
      <el-form-item label="系列" prop="series">
        <el-input v-model="queryParams.series" placeholder="请输入料号" />
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
      <el-col :span="1.5">
        <el-button type="success" :disabled="single" v-hasPermi="['fixture:edit']" plain icon="edit" @click="handleUpdate">
          {{ $t('btn.edit') }}
        </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="danger" :disabled="multiple" v-hasPermi="['fixture:delete']" plain icon="delete" @click="handleDelete">
          {{ $t('btn.delete') }}
        </el-button>
      </el-col>
      <el-col :span="1.5">
        <el-dropdown trigger="click" v-hasPermi="['fixture:import']">
          <el-button type="primary" plain icon="Upload">
            {{ $t('btn.import') }}<el-icon class="el-icon--right"><arrow-down /></el-icon>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="upload">
                <importData
                  templateUrl="fixture/fixtureBase/importTemplate"
                  importUrl="/fixture/fixtureBase/importData"
                  @success="handleFileSuccess"></importData>
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </el-col>
      <el-col :span="1.5">
        <el-button type="warning" plain icon="download" @click="handleExport" v-hasPermi="['fixture:export']">
          {{ $t('btn.export') }}
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
      @sort-change="sortChange"
      @selection-change="handleSelectionChange">
      <el-table-column type="selection" width="50" align="center" />
      <el-table-column prop="fixtureId" label="ID" align="center" v-if="columns.showColumn('fixtureId')" />
      <el-table-column prop="series" label="系列" min-width="150" align="center" v-if="columns.showColumn('series')" />
      <el-table-column
        prop="fixtureName"
        label="治具名称"
        min-width="160"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('fixtureName')" />
      <el-table-column
        prop="drawingNo"
        label="图纸编号"
        min-width="150"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('drawingNo')" />
      <el-table-column prop="price" label="单价" width="90" align="center" v-if="columns.showColumn('price')" />
      <el-table-column prop="safetyQty" label="安全库存" width="90" align="center" v-if="columns.showColumn('safetyQty')" />
      <el-table-column prop="totalQty" label="总库存" width="90" align="center" v-if="columns.showColumn('totalQty')" />
      <el-table-column prop="totalIdleQty" label="闲置数量" width="90" align="center" v-if="columns.showColumn('totalIdleQty')" />
      <el-table-column prop="totalUsingQty" label="占用数量" width="90" align="center" v-if="columns.showColumn('totalUsingQty')" />
      <el-table-column prop="qtyTip" label="库存显示" width="90" align="center" v-if="columns.showColumn('qtyTip')">
        <template #default="scope">
          <div v-if="scope.row.totalIdleQty + scope.row.totalUsingQty >= scope.row.safetyQty" class="ok">库存够用</div>
          <div v-if="scope.row.totalIdleQty + scope.row.totalUsingQty < scope.row.safetyQty" class="warn">赶快采购</div>
        </template>
      </el-table-column>
      <el-table-column prop="status" label="状态" width="70" align="center" v-if="columns.showColumn('status')">
        <template #default="scope">
          <dict-tag :options="options.sys_normal_disable" :value="scope.row.status" />
        </template>
      </el-table-column>
      <el-table-column
        prop="createBy"
        label="创建人"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('createBy')" />
      <el-table-column prop="createTime" label="创建时间" width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('createTime')" />
      <el-table-column
        prop="updateBy"
        label="更新人"
        width="90"
        align="center"
        :show-overflow-tooltip="true"
        v-if="columns.showColumn('updateBy')" />
      <el-table-column prop="updateTime" label="最后更新" width="160" :show-overflow-tooltip="true" v-if="columns.showColumn('updateTime')" />
      <el-table-column prop="remark" label="备注" align="center" :show-overflow-tooltip="true" v-if="columns.showColumn('remark')" />
      <el-table-column label="操作" fixed="right" width="160">
        <template #default="scope">
          <el-button type="primary" size="small" icon="view" title="详情" @click="handlePreview(scope.row)"></el-button>
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

    <!-- 添加或修改治具信息对话框 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="open">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :lg="12">
            <el-form-item label="治具名称" prop="fixtureName">
              <el-input v-model="form.fixtureName" placeholder="请输入治具名称" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="系列" prop="series">
              <template #label>
                <span>
                  <el-tooltip placement="top" content="图纸上的系列,通常为新产品的料号">
                    <el-icon> <questionFilled /> </el-icon>
                  </el-tooltip>
                  系列
                </span>
              </template>
              <el-select
                v-model="form.series"
                placeholder="请选择系列"
                clearable
                filterable
                remote
                :remote-method="handleQueryPart"
                class="fullWidth">
                <el-option v-for="item in options.part_options" :key="item.dictValue" :label="item.dictLabel" :value="item.dictLabel"></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="图纸编号" prop="drawingNo">
              <el-input v-model="form.drawingNo" placeholder="请输入图纸编号" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="单价" prop="price">
              <el-input-number v-model.number="form.price" :controls="true" controls-position="right" placeholder="请输入单价" class="fullWidth" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="安全库存" prop="safetyQty">
              <el-input-number
                v-model.number="form.safetyQty"
                :controls="true"
                controls-position="right"
                placeholder="请输入安全库存"
                class="fullWidth" />
            </el-form-item>
          </el-col>

          <el-col :lg="12">
            <el-form-item label="状态" prop="status">
              <el-radio-group v-model="form.status">
                <el-radio v-for="item in options.sys_normal_disable" :key="item.dictValue" :value="item.dictValue">
                  {{ item.dictLabel }}
                </el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>

          <el-col :lg="24">
            <el-form-item label="备注" prop="remark">
              <el-input type="textarea" v-model="form.remark" placeholder="请输入备注" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer v-if="opertype != 3">
        <el-button text @click="cancel">{{ $t('btn.cancel') }}</el-button>
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </template>
    </el-dialog>

    <!-- 详情 -->
    <el-dialog :title="title" :lock-scroll="false" v-model="openDetail" width="85%">
      <FixtureDetail :fixture-id="fixtureId" />
    </el-dialog>
  </div>
</template>

<script setup name="fixture">
import { listFixtureBase, listFixtureDetail, addFixtureBase, delFixtureBase, updateFixtureBase, getFixtureBase } from '@/api/fixture/fixtureBase.js'
import { dictPart } from '@/api/basic/part.js'
import importData from '@/components/ImportData'
import FixtureDetail from './FixtureDetail.vue'

const { proxy } = getCurrentInstance()
const router = useRouter()
const ids = ref([])
const loading = ref(false)
const showSearch = ref(true)
const table = ref(null)
const queryParams = reactive({
  pageNum: 1,
  pageSize: 10,
  sort: 'FixtureId',
  sortType: 'asc',
  fixtureName: undefined,
  series: undefined
})
const columns = ref([
  { visible: false, prop: 'fixtureId', label: 'ID' },
  { visible: true, prop: 'series', label: '系列' },
  { visible: true, prop: 'fixtureName', label: '治具名称' },
  { visible: false, prop: 'drawingNo', label: '图纸编号' },
  { visible: true, prop: 'price', label: '单价' },
  { visible: true, prop: 'safetyQty', label: '安全库存' },
  { visible: true, prop: 'totalQty', label: '总库存' },
  { visible: true, prop: 'totalIdleQty', label: '可用库存' },
  { visible: true, prop: 'totalUsingQty', label: '领用数量' },
  { visible: true, prop: 'qtyTip', label: '库存显示' },
  { visible: true, prop: 'status', label: '状态' },
  { visible: false, prop: 'createBy', label: '创建人' },
  { visible: false, prop: 'createTime', label: '创建时间' },
  { visible: false, prop: 'updateBy', label: '更新人' },
  { visible: false, prop: 'updateTime', label: '最后更新' },
  { visible: false, prop: 'remark', label: '备注' }
])
const total = ref(0)
const dataList = ref([])
const queryRef = ref()
const defaultTime = ref([new Date(2000, 1, 1, 0, 0, 0), new Date(2000, 2, 1, 23, 59, 59)])

var dictParams = [{ dictType: 'sys_normal_disable' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

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

function getList() {
  loading.value = true
  listFixtureDetail(queryParams).then((res) => {
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
// 多选框选中数据
function handleSelectionChange(selection) {
  ids.value = selection.map((item) => item.fixtureId)
  single.value = selection.length != 1
  multiple.value = !selection.length
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
    series: [{ required: true, message: '系列不能为空', trigger: 'change' }],
    fixtureName: [{ required: true, message: '治具名称不能为空', trigger: 'blur' }],
    price: [{ required: true, message: '单价不能为空', trigger: 'blur' }],
    safetyQty: [{ required: true, message: '安全库存不能为空', trigger: 'blur', type: 'number' }],
    status: [{ required: true, message: '状态不能为空', trigger: 'blur' }]
  },
  options: {
    // 系列 选项列表 格式 eg:{ partId:'0'}
    part_options: [],
    // 状态 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    sys_normal_disable: []
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
    series: null,
    fixtureName: null,
    drawingNo: null,
    price: null,
    safetyQty: null,
    status: null,
    createBy: null,
    createTime: null,
    updateBy: null,
    updateTime: null,
    remark: null
  }
  proxy.resetForm('formRef')
}

/**
 * 查看
 * @param {*} row
 */
const openDetail = ref(false)
const fixtureId = ref()
function handlePreview(row) {
  fixtureId.value = row.fixtureId
  openDetail.value = true
  title.value = '治具详情'
}

// 添加按钮操作
function handleAdd() {
  reset()
  open.value = true
  title.value = '添加治具信息'
  opertype.value = 1
}
// 修改按钮操作
function handleUpdate(row) {
  reset()
  const id = row.fixtureId || ids.value
  getFixtureBase(id).then((res) => {
    const { code, data } = res
    if (code == 200) {
      open.value = true
      title.value = '修改治具信息'
      opertype.value = 2

      form.value = {
        ...data
      }
    }
  })
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      if (form.value.fixtureId != undefined && opertype.value === 2) {
        updateFixtureBase(form.value).then((res) => {
          proxy.$modal.msgSuccess('修改成功')
          open.value = false
          getList()
        })
      } else {
        addFixtureBase(form.value).then((res) => {
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
  const Ids = row.fixtureId || ids.value

  proxy
    .$confirm('是否确认删除参数编号为"' + Ids + '"的数据项？', '警告', {
      confirmButtonText: proxy.$t('common.ok'),
      cancelButtonText: proxy.$t('common.cancel'),
      type: 'warning'
    })
    .then(function () {
      return delFixtureBase(Ids)
    })
    .then(() => {
      getList()
      proxy.$modal.msgSuccess('删除成功')
    })
}

// 导入数据成功处理
const handleFileSuccess = (response) => {
  const { item1, item2 } = response.data
  var error = ''
  item2.forEach((item) => {
    error += item.storageMessage + ','
  })
  proxy.$alert(item1 + '<p>' + error + '</p>', '导入结果', {
    dangerouslyUseHTMLString: true
  })
  getList()
}

// 导出按钮操作
function handleExport() {
  proxy
    .$confirm('是否确认导出治具表数据项?', '警告', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    .then(async () => {
      await proxy.downFile('/fixture/fixtureBase/export', { ...queryParams })
    })
}

handleQuery()
</script>

<style scoped>
.ok {
  background-color: #e1f3d8;
  color: #67c23a;
  border-radius: 4px;
  font-size: 12px;
}
.warn {
  background-color: rgb(248, 242, 212);
  color: rgb(224, 180, 23);
  border-radius: 4px;
  font-size: 12px;
}
</style>
