package com.lottery

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.TextView
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat

class ShareLotteryNumberActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContentView(R.layout.activity_share_lottery_number)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }

        val lotteryNumbersView: TextView = findViewById(R.id.lottery_number_generated)
        val userName: String = intent.getStringExtra("userName").toString()
        val shareButton : Button = findViewById(R.id.share_button)
        val lotteryNumbersGenerated: String = intent.getStringExtra("lotteryNumbers").toString()

        lotteryNumbersView.text = lotteryNumbersGenerated
        shareButton.setOnClickListener(View.OnClickListener{
            shareButton.setOnClickListener {
                val implicitIntent = Intent(Intent.ACTION_SEND).apply {
                    type = "*/*"
                    putExtra(Intent.EXTRA_TEXT, userName + buildString {
        append(" generates these lottery numbers \n The Lottery Numbers are: ")
    } + lotteryNumbersGenerated)
                }
                startActivity(implicitIntent)
            }
        })
    }


}