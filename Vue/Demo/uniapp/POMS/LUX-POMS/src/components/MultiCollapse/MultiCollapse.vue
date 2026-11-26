<template>
    <view class="multi-collapse">
        <view 
            v-for="(item, index) in menuData" 
            :key="item.id"
            class="collapse-item"
            :style="{ paddingLeft: level * 30 + 'rpx' }"
        >
            <!-- 菜单标题 -->
            <view 
                class="menu-header" 
                :class="{ 'is-open': item.open }"
                @click="toggleItem(item)"
            >
                <text class="menu-title">{{ item.title }}</text>
                <text class="menu-arrow" :class="{ 'arrow-rotate': item.open }">›</text>
            </view>

            <!-- 子菜单（递归） -->
            <view 
                class="menu-children" 
                :class="{ 'children-open': item.open }"
            >
                <MultiCollapse 
                    v-if="item.children && item.children.length > 0"
                    :menuData="item.children"
                    :level="level + 1"
                    @item-click="$emit('item-click', $event)"
                />
                
                <!-- 无子菜单时的内容 -->
                <view 
                    v-else 
                    class="menu-content"
                    @click="$emit('item-click', item)"
                >
                    <!-- <text>{{ item.content || '暂无内容' }}</text> -->
                </view>
            </view>
        </view>
    </view>
</template>

<script setup>
import { defineProps, defineEmits } from 'vue'

const props = defineProps({
    menuData: {
        type: Array,
        default: () => []
    },
    level: {
        type: Number,
        default: 0
    }
})

const emit = defineEmits(['item-click'])

// 切换菜单展开状态
const toggleItem = (item) => {
    if (item.children && item.children.length > 0) {
        // 有子菜单，切换展开状态
        item.open = !item.open
    } else {
        // 无子菜单，触发点击事件
        emit('item-click', item)
    }
}
</script>

<style lang="scss" scoped>
.multi-collapse {
    width: 100%;
    
    .collapse-item {
        margin-bottom: 2rpx;
        background-color: #fff;

        .menu-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 28rpx 25rpx;
            min-height: 90rpx;
            box-sizing: border-box;
            transition: background-color 0.2s;

            &:active {
                background-color: #f0f0f0;
            }

            &.is-open {
                background-color: #fafafa;
            }

            .menu-title {
                font-size: 28rpx;
                color: #333;
            }

            .menu-arrow {
                font-size: 36rpx;
                color: #999;
                transition: transform 0.3s ease;

                &.arrow-rotate {
                    transform: rotate(90deg);
                }
            }
        }

        .menu-children {
            max-height: 0;
            overflow: hidden;
            transition: max-height 0.3s ease;

            &.children-open {
                max-height: 2000rpx;  // 设置足够大的值
            }

            .menu-content {
                padding: 25rpx 25rpx;
                background-color: #fafafa;
                border-left: 6rpx solid #eee;
                margin-left: 6rpx;

                &:active {
                    background-color: #f0f0f0;
                }

                text {
                    font-size: 26rpx;
                    color: #666;
                    line-height: 1.6;
                }
            }
        }
    }
}
</style>