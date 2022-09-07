import { NativeStackScreenProps } from "@react-navigation/native-stack";

type RootStackParamList = {
    Login: undefined;
    Register: undefined;
};
  
type LoginNavigation = NativeStackScreenProps<RootStackParamList, "Login">;
type RegisterNavigation = NativeStackScreenProps<RootStackParamList, "Register">;

export { RootStackParamList, LoginNavigation, RegisterNavigation };
