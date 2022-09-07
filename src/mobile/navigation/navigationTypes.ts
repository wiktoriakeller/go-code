import { NativeStackScreenProps } from "@react-navigation/native-stack";

type RootStackParamList = {
    SignIn: undefined;
    SignUp: undefined;
};
  
type SignInNavigation = NativeStackScreenProps<RootStackParamList, "SignIn">;
type SignUpNavigation = NativeStackScreenProps<RootStackParamList, "SignUp">;

export { RootStackParamList, SignUpNavigation, SignInNavigation };
