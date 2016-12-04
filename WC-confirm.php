<?php

	$WebsiteRoot=$_SERVER['DOCUMENT_ROOT'];

	include_once($WebsiteRoot . '/wp-config.php');
	if ( in_array( 'woocommerce/woocommerce.php', apply_filters( 'active_plugins', get_option( 'active_plugins' ) ) ) ) {
		ini_set('display_errors', 1);
		error_reporting(E_ALL|E_STRICT);
		
		global $woocommerce;
		
		if (is_user_logged_in()){
			$userid = get_user_meta( $current_user->ID, 'user_login', true );
			$fname = get_user_meta( $current_user->ID, 'first_name', true );
			$lname = get_user_meta( $current_user->ID, 'last_name', true );
			$company = get_user_meta( $current_user->ID, 'billing_company', true ); 
			$address_1 = get_user_meta( $current_user->ID, 'billing_address_1', true ); 
			$address_2 = get_user_meta( $current_user->ID, 'billing_address_2', true );
			$phonenumber = get_user_meta( $current_user->ID, 'billing_phone', true);
			$country = get_user_meta( $current_user->ID, 'shipping_country', true );
			$city = get_user_meta( $current_user->ID, 'billing_city', true );
			$postcode = get_user_meta( $current_user->ID, 'billing_postcode', true );
			$email = get_user_meta( $current_user->ID, 'billing_email', true );
			
			echo "Username = ".$userid."\n";
			echo "email = ".$email."\n\n";
			
			echo "Billing address:\n\n";
			echo "First name: ".$fname."\n";
			echo "Last name: ".$lname."\n";
			echo "Company: ".$company."\n";
			echo "Address: ".$address_1."\n";
			echo "Address 2: ".$address_2."\n";
			echo "Phone number: ".$phonenumber."\n";
			echo "country: ".$country."\n";
			echo "city: ".$city."\n";
			echo "postcode: ".$postcode."\n\n\n";
			
			$fnameS = get_user_meta( $current_user->ID, 'shipping_first_name', true );
			$lnameS = get_user_meta( $current_user->ID, 'shipping_last_name', true );
			$companyS = get_user_meta( $current_user->ID, 'shipping_company', true );
			$address_1S = get_user_meta( $current_user->ID, 'shipping_address_1', true );
			$address_2S = get_user_meta( $current_user->ID, 'shipping_address_2', true );
			$cityS = get_user_meta( $current_user->ID, 'shipping_city', true );
			$postcodeS = get_user_meta( $current_user->ID, 'shipping_postcode', true );
			$countryS = get_user_meta( $current_user->ID, 'shipping_country', true );
			
			echo "Shipping address\n\n";
			echo "First name: ".$fnameS."\n";
			echo "Last name: ".$lnameS."\n";
			echo "Company: ".$companyS."\n";
			echo "Address: ".$address_1S."\n";
			echo "Address 2: ".$address_2S."\n";
			echo "country: ".$countryS."\n";
			echo "city: ".$cityS."\n";
			echo "postcode: ".$postcodeS."\n\n";
			
			echo "please make sure that the information is correct before processding";
			
			$products = $_SERVER['QUERY_STRING']; 
			
		} else {
			echo "Sorry you have to login! \n Go back to the VR-store and checkout again after you have logged in!";
		}
	}
?>
<html>
		<form action="WC-VR2.php/?<?php $products; ?>" method="post">
			<input type='submit' name='submit' value='Register' class='register' />
		</form>
		</html>