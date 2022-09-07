import SignInPage from "./pages/SignInPage";
import RegisterPage from "./pages/RegisterPage";
import ForgotPasswordPage from "./pages/ForgotPasswordPage";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { RootStackParamList } from "./navigation/navigationTypes";

export default function App() {
  const Stack = createNativeStackNavigator<RootStackParamList>();

  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Login">
        <Stack.Screen 
          name="Login" 
          component={SignInPage} 
        />
        <Stack.Screen 
          name="Register" 
          component={RegisterPage} 
        />
        <Stack.Screen 
          name="ForgotPassword" 
          component={ForgotPasswordPage} 
          options={{ title: "Reset password"}}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
