<?php
	/*
	if ( ! defined( 'ABSPATH' ) ) {
	exit; // Exit if accessed directly
	}*/
	$WebsiteRoot=$_SERVER['DOCUMENT_ROOT'];
	//include_once($WebsiteRoot . '/wp-includes/option.php');
	include_once($WebsiteRoot . '/wp-config.php');
	if ( in_array( 'woocommerce/woocommerce.php', apply_filters( 'active_plugins', get_option( 'active_plugins' ) ) ) ) {
		ini_set('display_errors', 1);
		error_reporting(E_ALL|E_STRICT);
		//require_once(dirname(__FILE__) . '/../../../wp-load.php');
		//?product1=472&productQ1=1&product2=556&productQ2=1
		//http://fiskeapp.se/wp-content/plugins/wc_vr/WC-VR.php/?product1=472&productQ1=1&product2=556&productQ2=1
		//require_once( $WebsiteRoot . '/wp-content/plugins/woocommerce/includes/class-wc-cart.php' );
		//require_once( $WebsiteRoot . '/wp-content/themes/ktzunyporto/functions.php' );
		//require('/the/path/to/your/wp-blog-header.php');
		
		global $woocommerce;
		
		//print_r($woocommerce);
		function parseQueryString(){
			$products = $_SERVER['QUERY_STRING']; 
			
			parse_str($products, $get_array);
			
			return $get_array;
		}
		
		$parameters = parseQueryString();
		print_r($parameters);
		$product_id = 0;
		$order = wc_create_order();
		foreach($parameters as $key => $value){
			
			if (substr($key,0,8) === "productQ"){
				$product_id_int = intval($product_id);
	
				$value_int = intval($value);
				//$woocommerce->cart->add_to_cart($product_id_int,$value_int);
				$order->add_product( get_product($product_id_int), $value_int );
				print_r("product ID: " + $product_id + "value: " + $value);
			} elseif (substr($key,0,7) === "product"){
				$product_id = $value;
				print_r("first: " + $product_id);
				
			}
		}
		$userid = get_user_meta( $current_user->ID, 'user_login', true );
		echo $userid;
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
		
		$addressB = array(
        'first_name' => $fname,
        'last_name'  => $lname,
        'company'    => $company,
        'email'      => $email,
        'phone'      => $phonenumber,
        'address_1'  => $address_1,
        'address_2'  => $address_2, 
        'city'       => $city,
        'postcode'   => $postcode,
        'country'    => $country,
		'customer_user' => $current_user->ID
		);

		/*
		echo $fname . "<BR>";
		echo $lname . "<BR>";
		echo $company . "<BR>";
		echo $phonenumber . "<BR>";
		echo $address_1 . "<BR>";
		echo $address_2 . "<BR>";
		echo $country . "<BR>";
		echo $city . "<BR>";
		echo $postcode . "<BR>";
		*/
		
		$fnameS = get_user_meta( $current_user->ID, 'shipping_first_name', true );
		$lnameS = get_user_meta( $current_user->ID, 'shipping_last_name', true );
		$companyS = get_user_meta( $current_user->ID, 'shipping_company', true );
		$address_1S = get_user_meta( $current_user->ID, 'shipping_address_1', true );
		$address_2S = get_user_meta( $current_user->ID, 'shipping_address_2', true );
		$cityS = get_user_meta( $current_user->ID, 'shipping_city', true );
		$postcodeS = get_user_meta( $current_user->ID, 'shipping_postcode', true );
		$countryS = get_user_meta( $current_user->ID, 'shipping_country', true );
		
		$addressS = array(
        'first_name' => $fnameS,
        'last_name'  => $lnameS,
        'company'    => $companyS,
        'address_1'  => $address_1S,
        'address_2'  => $address_2S, 
        'city'       => $cityS,
        'postcode'   => $postcodeS,
        'country'    => $countryS
		);
		
		//header("Location: http://fiskeapp.se/checkout/");
		//die();
		/*
		echo $fnameS . "<BR>";
		echo $lnameS . "<BR>";
		echo $companyS . "<BR>";
		echo $address_1S . "<BR>";
		echo $address_2S . "<BR>";
		echo $countryS . "<BR>";
		echo $cityS . "<BR>";
		echo $postcodeS . "<BR>";
		*/
		
		$order->set_address( $addressB, 'billing' );
		$order->set_address( $addressS, 'shipping' );
		
		
		$order->calculate_totals();
		
		update_post_meta( $order->id, '_payment_method', 'cheque' );
		update_post_meta( $order->id, '_payment_method_title', 'cheque' );
		update_post_meta( $order->id, '_customer_user', $current_user->ID);
		// Store Order ID in session so it can be re-used after payment failure
		WC()->session->order_awaiting_payment = $order->id;

		// Process Payment
		$available_gateways = WC()->payment_gateways->get_available_payment_gateways();
		$result = $available_gateways[ 'cheque' ]->process_payment( $order->id );

		// Redirect to success/confirmation/payment page
		if ( $result['result'] == 'success' ) {

			$result = apply_filters( 'woocommerce_payment_successful_result', $result, $order->id );

			wp_redirect( $result['redirect'] );
        exit;
    }
		
	}
		

		

?>
