<template>
    <view class="workbenches">
        <!-- 菜单区域 -->
        <view class="menu-container">
            <MultiCollapse 
                :menuData="menuData"
                @item-click="onMenuClick"
            />
        </view>
    </view>
</template>

<script setup>
import { ref, reactive, computed } from 'vue'

// 菜单数据
const menuData = ref([
    {
        id: "1",
        title: '系统管理',
        open: false,
        children: [
            {
                id: "1-1",
                title: '系统资料',
                open: false,
                children: [
                    { id: "1-1-1", title: '线别', page_name: 'line' },
                    { id: "1-1-2", title: '部门', page_name: 'department' },
                    { id: "1-1-3", title: '分组', page_name: 'usergroup' },
                    { id: "1-1-4", title: '角色', page_name: 'role' },
                    { id: "1-1-5", title: '模块管理', page_name: 'model' },
                    { id: "1-1-6", title: '员工', page_name: 'user' },
                ]
            },
            {
                id: "1-2",
                title: '仓库管理',
                open: false,
                children: [
                    { id: 121, title: '仓位管理', page_name: 'ck' },
                    { id: 122, title: '储位管理', page_name: 'cw' },
                ]
            }
        ]
    },
    {
        id: 2,
        title: '业务模块',
        open: false,
        children: [
            { id: 21, title: '订单管理', page_name: '订单管理详情...' },
            { id: 22, title: '产品管理', page_name: '产品管理详情...' },
        ]
    },
    {
        id: 3,
        title: '设置',
        open: false,
        page_name: '设置页面内容'
    }
])


// 菜单选项
const menuOptions = computed(() => {
    const options = []
    const flatten = (menus, level = 0) => {
        menus.forEach(menu => {
            options.push({
                id: menu.id,
                title: '├ '.repeat(level) + menu.title
            })
            if (menu.children && menu.children.length > 0) {
                flatten(menu.children, level + 1)
            }
        })
    }
    flatten(menuData.value)
    return options
})

const onParentChange = (e) => {
    const index = e.detail.value
    const selected = menuOptions.value[index]
    newMenu.parentId = selected.id
    selectedParentTitle.value = selected.title
}

const openAddModal = () => {
    newMenu.title = ''
    newMenu.page_name = ''
    newMenu.parentId = null
    selectedParentTitle.value = ''
    addPopup.value.open()
}

const closeAddModal = () => {
    addPopup.value.close()
}


const onMenuClick = (item) => {
    uni.navigateTo({
        url:"/pages/"+item.page_name+"/"+item.page_name
    })
    // console.log('点击菜单:', item)
    // uni.showToast({ title: item.title, icon: 'none' })
}
</script>

<style lang="scss" scoped>
.workbenches {
    height: 100vh;
    display: flex;
    flex-direction: column;
    background-color: #f5f5f5;

    .menu-container {
        flex: 1;
        overflow-y: auto;
        padding: 25rpx;
    }

}
</style>