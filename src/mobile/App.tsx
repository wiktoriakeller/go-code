import { StyleSheet, View } from 'react-native';
import SignInScreen from './pages/SignInPage';

export default function App() {
    return (
    <View style={styles.root}>
        <SignInScreen/>
    </View>
  );
}

const styles = StyleSheet.create({
    root: {
        flex: 1,
        backgroundColor: '#f9fbfc'
    },
});
