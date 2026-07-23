import { dictLine } from '@/api/basic/line.js'
import { treeConsumableStorageSpace } from '@/api/consumable/consumableStorageSpace.js'
import { treeFixtureStorageSpace } from '@/api/fixture/fixtureStorageSpace.js'
import { getFactoryConfigBykey } from '@/api/basic/factoryConfig.js'

const useBasicStore = defineStore('basic', {
  state: () => ({
    factoryConfigs: [],
    isInitLine: false,
    lineDict: [],
    isInitConsumableStorage: false,
    consumableStorageTree: [],
    isInitFixtureStorage: false,
    fixtureStorageTree: []
  }),
  getters: {
    getLineDict: (store) => {
      if (!store.isInitLine) store.initLine()
      return store.lineDict
    },
    getConsumableStorageTree: (store) => {
      if (!store.isInitConsumableStorage) store.initConsumableStorge()
      return store.consumableStorageTree
    },
    getFixtureStorageTree: (store) => {
      if (!store.isInitFixtureStorage) store.initFixtureStorge()
      return store.fixtureStorageTree
    }
  },
  actions: {
    //获取指定key的厂区配置
    async getFactoryConfig(key) {
      let fc = this.factoryConfigs.find((item) => item.configKey === key)
      if (!fc)
        await getFactoryConfigBykey(key)
          .then((res) => {
            const { code, data } = res
            if (code == 200) {
              this.factoryConfigs.push(...data)
              fc = data[0]
            }
          })
          .catch(() => {})
      return fc //then处理
    },
    //初始产线信息
    async initLine() {
      await this.updateLine()
      this.isInitLine = true
    },
    //更新产线信息
    updateLine() {
      const query = {
        pageNum: 1,
        pageSize: 1000,
        sort: '',
        sortType: 'asc'
      }
      return dictLine(query)
        .then((res) => {
          const { code, data } = res
          if (code == 200) {
            this.lineDict = data.result
          }
        })
        .catch(() => {})
    },
    //初始化耗品储位信息
    async initConsumableStorge() {
      await this.updateConsumableStorage()
      this.isInitConsumableStorage = true
    },
    //更新耗品储位信息
    async updateConsumableStorage() {
      await treeConsumableStorageSpace()
        .then((res) => {
          const { code, data } = res
          if (code == 200) {
            this.consumableStorageTree = data
          }
        })
        .catch(() => {})
    },
    //初始化治具储位信息
    async initFixtureStorge() {
      await this.updateFixtureStorage()
      this.isInitFixtureStorage = true
    },
    //更新治具储位信息
    async updateFixtureStorage() {
      await treeFixtureStorageSpace()
        .then((res) => {
          const { code, data } = res
          if (code == 200) {
            this.fixtureStorageTree = data
          }
        })
        .catch(() => {})
    }
  }
})

export default useBasicStore
