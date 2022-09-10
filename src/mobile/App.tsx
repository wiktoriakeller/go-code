import SignInPage from "./pages/SignInPage";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { RootStackParamList } from "./navigation/navigationTypes";
import SignUpPage from "./pages/SignUpPage";

export default function App() {
  const Stack = createNativeStackNavigator<RootStackParamList>();

  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="SignIn">
        <Stack.Screen 
          name="SignIn" 
          component={SignInPage}
          options={{ title: "Sign In"}}
        />
        <Stack.Screen 
          name="SignUp" 
          component={SignUpPage} 
          options={{ title: "Sign Up"}}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
