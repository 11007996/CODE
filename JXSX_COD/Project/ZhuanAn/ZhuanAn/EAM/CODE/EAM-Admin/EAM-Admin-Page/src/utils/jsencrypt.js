import JSEncrypt from 'jsencrypt/bin/jsencrypt.min'

// 密钥对生成 http://web.chacuo.net/netrsakeypair

const publicKey = 'MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAOghKKXd1RmmSt49I9BlvUjfK7PNAlljl79GgyTlaX722pdy8uDO7XNSuxfyRhuXiYPMZXUDSd4DV+SMiX/vDgMCAwEAAQ=='

const privateKey =
  'MIIBVwIBADANBgkqhkiG9w0BAQEFAASCAUEwggE9AgEAAkEA6CEopd3VGaZK3j0j0GW9SN8rs80CWWOXv0aDJOVpfvbal3Ly4M7tc1K7F/JGG5eJg8xldQNJ3gNX5IyJf+8OAwIDAQABAkEAsBTfEZrJWHCVICm+rglO4SUwsG4Vlxr99AEX3/gJ410Jl+quZD0c/N1KT+HtDG+zAMSMJ//V3ajjKuIiN+S8qQIhAPnUGW/Jwbs1JfcR9RYnhP7WZrPgyKbZDabmsbFWm4XNAiEA7d0h0BeAikEMANld6r8i4ldMhzX2Fik0Hs/x60V0Ew8CIQCIfFOZOwRYHmUrYegfvl9uSfu58egtHw/SSt5xH/u/UQIhAMx2PrBu41D0JOs97VrxvXyt6dx35/aYqHKr8Jl59BLFAiEAm6J9zB/+VfrLJ4RsCvwVP9FdT7x+hwxlt06OwzxvEaQ='

// 加密
export function encrypt(txt) {
  const encryptor = new JSEncrypt()
  encryptor.setPublicKey(publicKey) // 设置公钥
  return encryptor.encrypt(txt) // 对数据进行加密
}

// 解密
export function decrypt(txt) {
  const encryptor = new JSEncrypt()
  encryptor.setPrivateKey(privateKey) // 设置私钥
  return encryptor.decrypt(txt) // 对数据进行解密
}

// 自定义公钥加密
export function cusEncrypt(txt, publicKey) {
  const encryptor = new JSEncrypt()
  encryptor.setPublicKey(publicKey) // 设置公钥
  return encryptor.encrypt(txt) // 对数据进行加密
}
