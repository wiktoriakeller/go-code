import { NativeStackScreenProps } from "@react-navigation/native-stack";

type RootStackParamList = {
    SignIn: undefined;
    SignUp: undefined;
    Home: undefined;
};
  
type SignInNavigation = NativeStackScreenProps<RootStackParamList, "SignIn">;
type SignUpNavigation = NativeStackScreenProps<RootStackParamList, "SignUp">;
type HomeNavigation = NativeStackScreenProps<RootStackParamList, "Home">;

export { RootStackParamList, SignUpNavigation, SignInNavigation, HomeNavigation };
