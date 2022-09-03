import { StyleSheet, View } from "react-native";
import SignInScreen from "./pages/SignInPage";
import colors from "./styles/colors";

export default function App() {
  return (
    <View style={styles.root}>
      <SignInScreen />
    </View>
  );
}

const styles = StyleSheet.create({
  root: {
    flex: 1,
    backgroundColor: colors.background
  },
});
