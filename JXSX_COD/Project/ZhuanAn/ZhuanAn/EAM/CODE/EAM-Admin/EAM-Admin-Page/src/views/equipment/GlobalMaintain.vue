<!--
 * @Descripttion: (全局保养维护)
 * @Author: (admin)
 * @Date: (2024-10-07)
-->
<template>
  <div v-loading="loading" element-loading-text="耗时操作，请耐心等待">
    <!-- 提示 -->
    <div class="custom-block warning">
      <span style="font-weight: bold">提示</span>
      <li>如果资产对应年份的保养报表不存在，则不会添加相关数据</li>
      <li>如原有记录存在，未填入值的项目则会补全,也可通过‘强制覆盖’来覆盖原有记录,特殊值不会覆盖</li>
      <li>保养人最少需要一个，也可以选择多个来循环设置保养记录的保养人签名</li>
      <li>如果未选择成本中心或指定设备，则当前操作会作用于所有资产，请谨慎操作</li>
    </div>

    <!-- 表单 -->
    <el-form ref="formRef" :model="form" :rules="rules" label-width="100px">
      <el-row :gutter="20">
        <el-col :lg="12">
          <el-form-item label="保养日期" prop="maintainDate">
            <el-date-picker v-model="form.maintainDate" type="date" placeholder="保养日期选择" />
          </el-form-item>
        </el-col>

        <el-col :lg="12">
          <el-form-item label="日期标记" prop="dateMark">
            <el-select v-model="form.dateMark" placeholder="请选择日期标记">
              <el-option v-for="item in options.date_mark" :key="item.dictValue" :label="item.dictLabel" :value="item.dictValue"></el-option>
            </el-select>
          </el-form-item>
        </el-col>

        <el-col :lg="12">
          <el-form-item label="成本中心" prop="costCenter">
            <el-select v-model="form.costCenter" placeholder="请选择成本中心">
              <el-option v-for="item in options.costCenter_options" :label="item.dictLabel" :value="item.dictValue"></el-option>
            </el-select>
          </el-form-item>
        </el-col>

        <el-col :lg="24">
          <el-form-item label="资产编号" prop="equipmentList">
            <el-select
              v-model="form.equipmentList"
              placeholder="资产编号/设备名称/资产名称/自定义机型；如果不选择表示所有设备"
              clearable
              filterable
              multiple
              remote
              :remote-method="handleQueryEquipment"
              class="fullWidth">
              <el-option
                v-for="item in options.equipment_options"
                :key="item.dictValue"
                :label="item.dictLabel"
                :value="item.dictValue"></el-option>
            </el-select>
          </el-form-item>
        </el-col>

        <el-col :lg="12">
          <el-form-item label="部门" prop="deptId">
            <el-tree-select
              v-model="form.deptId"
              :data="options.dept_tree_options"
              :props="{ value: 'id', label: 'label', children: 'children' }"
              value-key="id"
              placeholder="请选择归属部门"
              check-strictly
              :render-after-expand="false" />
          </el-form-item>
        </el-col>

        <el-col :lg="24">
          <el-form-item label="保养人" prop="executorList">
            <el-select
              v-model="form.executorList"
              value-key="dictValue"
              placeholder="请输入员工姓名"
              clearable
              filterable
              multiple
              remote
              :remote-method="handleQueryEmployee"
              class="fullWidth">
              <el-option
                v-for="item in options.emp_options"
                :key="item.dictValue"
                :label="item.dictValue + ' - ' + item.dictLabel"
                :value="item.dictValue"></el-option>
            </el-select>
          </el-form-item>
        </el-col>

        <el-col :lg="24">
          <el-form-item label="强制覆盖" prop="cover">
            <el-switch v-model="form.cover" inline-prompt active-text="是" inactive-text="否" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row style="justify-content: end; margin-top: 10px">
        <el-button type="primary" @click="submitForm">{{ $t('btn.submit') }}</el-button>
      </el-row>
    </el-form>
  </div>
</template>
<script setup name="globalmaintain">
import { globalMaintainRecord } from '@/api/equipment/maintainRecord.js'
import { dictEquipmentBase, dictCostCenter } from '@/api/equipment/equipmentBase.js'
import { dictEmployee } from '@/api/basic/employee.js'
import { factoryDeptTreeselect } from '@/api/system/dept.js'

const { proxy } = getCurrentInstance()
const loading = ref(false)
const dictParams = [{ dictType: 'date_mark' }]

proxy.getDicts(dictParams).then((response) => {
  response.data.forEach((element) => {
    state.options[element.dictType] = element.list
  })
})

/*************** form操作 ***************/
const formRef = ref()
const state = reactive({
  form: {},
  rules: {
    dateMark: [{ required: true, message: '日期标记不能为空', trigger: 'change' }],
    maintainDate: [{ required: true, message: '保养日期不能为空', trigger: 'blur' }]
  },
  options: {
    // 日期标记 选项列表 格式 eg:{ dictLabel: '标签', dictValue: '0'}
    date_mark: [],
    // 资产编号 选项列表
    equipment_options: [],
    // 员工列表
    emp_options: [],
    //成本中心
    costCenter_options: [],
    //部门树
    dept_tree_options: []
  }
})

const { form, rules, options } = toRefs(state)

// 重置表单
function reset() {
  form.value = {
    dateMark: null,
    maintainDate: null,
    equipmentList: [],
    executorList: []
  }
  options.equipment_options = []
  proxy.resetForm('formRef')
}

// 添加&修改 表单提交
function submitForm() {
  proxy.$refs['formRef'].validate((valid) => {
    if (valid) {
      loading.value = true
      globalMaintainRecord(form.value)
        .then((res) => {
          proxy.$modal.msgSuccess('全局保养成功')
          loading.value = false
        })
        .catch((e) => {
          loading.value = false
        })
    }
  })
}

// 查询资产编号
function handleQueryEquipment(keyword) {
  if (keyword) {
    const params = {
      pageNum: 1,
      pageSize: 10,
      sort: '',
      sortType: 'asc',
      keyword: keyword
    }
    setTimeout(() => {
      dictEquipmentBase(params).then((res) => {
        state.options.equipment_options = res.data.result
      })
    }, 200)
  }
}

//员工查询
function handleQueryEmployee(keyword) {
  if (keyword) {
    const query = {
      pageNum: 1,
      pageSize: 10,
      empName: keyword
    }
    setTimeout(() => {
      dictEmployee(query).then((res) => {
        options.value.emp_options = res.data.result
      })
    }, 200)
  }
}

//成本中心字典
function handleQueryCostCenterDict() {
  const query = {
    pageNum: 1,
    pageSize: 100
  }
  setTimeout(() => {
    dictCostCenter(query).then((res) => {
      options.value.costCenter_options = res.data.result
    })
  }, 200)
}

/** 查询部门下拉树结构 */
function getTreeselect() {
  factoryDeptTreeselect().then((res) => {
    options.value.dept_tree_options = res.data
  })
}

getTreeselect()
handleQueryCostCenterDict()
</script>

<style lang="scss" scoped>
.custom-block.warning {
  padding: 8px 16px;
  background-color: rgb(245, 108, 108, 0.1);
  border-radius: 4px;
  border-left: 5px solid var(--el-color-danger);
  margin: 20px 0;
}
</style>
