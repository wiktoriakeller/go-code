import AsyncStorage from '@react-native-async-storage/async-storage';

const storeData = async (key: string, value: string) => {
  try {
    await AsyncStorage.setItem(`@${key}`, value);
    return true;
  }
  catch(error) {
    return false;
  }
}

const getData = async (key: string) => {
  try {
    const value = await AsyncStorage.getItem(`@${key}`);
    return value;
  }
  catch(error) {
    return null;
  }
}

export { storeData, getData };
